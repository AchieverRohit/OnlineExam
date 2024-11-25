
using thinkschool.OnlineExam.Core.Validations;

namespace thinkschool.OnlineExam.Services.Services;
public class QuestionService : IQuestionService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public QuestionService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allQuestions or fetchesQuestions with pagination based on requestDto
     public async Task<ListResponse<QuestionResDto>> GetAll(GetAllQuestionReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<Question>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<Question, GetAllQuestionReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<QuestionResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<QuestionResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new Question to the database
     public async Task<SingleResponse<QuestionResDto>> Save(AddQuestionReqDto requestDto)
     {
        var entity = _mapper.Map<Question>(requestDto); // Maps the request DTO to the Question entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<QuestionResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<QuestionResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing Question in the database
     public async Task<SingleResponse<QuestionResDto>> Update(UpdateQuestionReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<Question,int>(requestDto.QuestionId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.QuestionId)); // Fetches the existing Question by QuestionId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<QuestionResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<QuestionResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a Question by QuestionId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int QuestionId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<Question>(x => x.QuestionId == QuestionId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, QuestionId));// Fetches the  Question by QuestionId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<QuestionResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<QuestionResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  Question record
     public async Task<BaseResponse> Delete(int QuestionId)
     {
        var existing = await _baseRepository.GetByIdAsync<Question, int>(QuestionId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, QuestionId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }


    /// <summary>
    /// Adds a new question with options to the database.
    /// </summary>
    /// <param name="questionDto">The question DTO containing the details of the question and options.</param>
    /// <param name="cancellationToken">Cancellation token for async operations.</param>
    /// <returns>Returns a SingleResponse containing the added QuestionDto.</returns>
    /// <exception cref="ValidationException">Thrown when validation of the QuestionDto fails.</exception>
    public async Task<SingleResponse<QuestionDto>> AddQuestionWithOptions(QuestionDto questionDto, CancellationToken cancellationToken)
    {
        if (questionDto == null) throw new ArgumentNullException(nameof(questionDto));

        // Validate the QuestionDto
        var validator = new QuestionDtoValidator();
        var validationResult = validator.Validate(questionDto);

        if (!validationResult.IsValid)
        {
            return new SingleResponse<QuestionDto>
            {
                Status = HttpStatusCode.BadRequest,
                Messages = validationResult.Errors
                    .Select(e => new ResponseMessage { Message = e.ErrorMessage })
                    .ToList()
            };
        }

        try
        {
            // Map the QuestionDto to Question Entity
            var question = new Question
            {
                CreatedBy = questionDto.CreatedBy,
                IsFromQuestionBank = questionDto.IsFromQuestionBank,
                IsMedia = questionDto.IsMedia,
                IsMultipleChoice = questionDto.IsMultipleChoice,
                MediaType = questionDto.MediaType,
                MediaURL = questionDto.MediaURL,
                QuestionMaxMarks = questionDto.QuestionMaxMarks,
                QuestionText = questionDto.QuestionText,
                SectionId = questionDto.SectionId
            };

            // Add the Question to the DbContext
            await _dbContext.Questions.AddAsync(question, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken); // Save to generate QuestionId

            // Add Options
            foreach (var optionDto in questionDto.Options)
            {
                var option = new Option
                {
                    QuestionId = question.QuestionId, // Use the saved QuestionId
                    IsCorrect = optionDto.IsCorrect,
                    Marks = optionDto.Marks,
                    OptionText = optionDto.OptionText // Correctly map OptionText
                };

                await _dbContext.Options.AddAsync(option, cancellationToken);
            }

            // Save all Options
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Return Response
            return new SingleResponse<QuestionDto>
            {
                Status = HttpStatusCode.Created,
                Data = questionDto
            };
        }
        catch (Exception ex)
        {
            return new SingleResponse<QuestionDto>
            {
                Status = HttpStatusCode.InternalServerError,
                Messages = new List<ResponseMessage>
            {
                new ResponseMessage { Message = ex.Message }
            }
            };
        }
    }

}



