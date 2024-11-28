
using thinkschool.OnlineExam.Core.Models.UserAnswerDtos;

namespace thinkschool.OnlineExam.Services.Services;
public class UserAnswerService : IUserAnswerService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public UserAnswerService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allUserAnswers or fetchesUserAnswers with pagination based on requestDto
     public async Task<ListResponse<UserAnswerResDto>> GetAll(GetAllUserAnswerReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<UserAnswer>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<UserAnswer, GetAllUserAnswerReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<UserAnswerResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<UserAnswerResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new UserAnswer to the database
     public async Task<SingleResponse<UserAnswerResDto>> Save(AddUserAnswerReqDto requestDto)
     {
        var entity = _mapper.Map<UserAnswer>(requestDto); // Maps the request DTO to the UserAnswer entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<UserAnswerResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<UserAnswerResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing UserAnswer in the database
     public async Task<SingleResponse<UserAnswerResDto>> Update(UpdateUserAnswerReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<UserAnswer,int>(requestDto.UserAnswerId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.UserAnswerId)); // Fetches the existing UserAnswer by UserAnswerId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<UserAnswerResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<UserAnswerResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a UserAnswer by UserAnswerId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int UserAnswerId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<UserAnswer>(x => x.UserAnswerId == UserAnswerId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, UserAnswerId));// Fetches the  UserAnswer by UserAnswerId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<UserAnswerResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<UserAnswerResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  UserAnswer record
     public async Task<BaseResponse> Delete(int UserAnswerId)
     {
        var existing = await _baseRepository.GetByIdAsync<UserAnswer, int>(UserAnswerId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, UserAnswerId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }

    /// <summary>
    /// Submits an answer for a given question.
    /// </summary>
    /// <param name="submitAnswerDto">Data transfer object containing answer details.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A response indicating the submission result.</returns>
    /// <exception cref="ArgumentNullException">Throws if submitAnswerDto is null.</exception>
    public async Task<SingleResponse<string>> SubmitAnswer(SubmitAnswerDto submitAnswerDto, CancellationToken cancellationToken)
    {
        if (submitAnswerDto == null) throw new ArgumentNullException(nameof(submitAnswerDto));
        try
        {
            var userAnswer = new UserAnswer
            {
                QuestionId = submitAnswerDto.QuestionId,
                UserExamId = submitAnswerDto.UserExamId,
                SectionId = submitAnswerDto.SectionId
            };

            _dbContext.UserAnswers.Add(userAnswer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (submitAnswerDto.OptionIds.Count > 0)
            {
                var userAnswerOptions = submitAnswerDto.OptionIds.Select(optionId => new UserAnswerOption
                {
                    UserAnswerId = userAnswer.UserAnswerId,
                    OptionId = optionId
                }).ToList();

                _dbContext.UserAnswerOptions.AddRange(userAnswerOptions);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            

            return new SingleResponse<string>
            {
                Data = "Answer submitted successfully.",
                Status = HttpStatusCode.OK
            };
        }
        catch (Exception ex)
        {
            // Log the exception (consider using a logging framework)
            return new SingleResponse<string>
            {
                Data = "An error occurred while submitting the answer.",
                Status = HttpStatusCode.InternalServerError
            };
        }
    }

}



