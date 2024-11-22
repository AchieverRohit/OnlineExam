
namespace thinkschool.OnlineExam.Services.Services;
public class SectionResultService : ISectionResultService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public SectionResultService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allSectionResults or fetchesSectionResults with pagination based on requestDto
     public async Task<ListResponse<SectionResultResDto>> GetAll(GetAllSectionResultReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<SectionResult>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<SectionResult, GetAllSectionResultReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<SectionResultResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<SectionResultResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new SectionResult to the database
     public async Task<SingleResponse<SectionResultResDto>> Save(AddSectionResultReqDto requestDto)
     {
        var entity = _mapper.Map<SectionResult>(requestDto); // Maps the request DTO to the SectionResult entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<SectionResultResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<SectionResultResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing SectionResult in the database
     public async Task<SingleResponse<SectionResultResDto>> Update(UpdateSectionResultReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<SectionResult,int>(requestDto.SectionResultId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.SectionResultId)); // Fetches the existing SectionResult by SectionResultId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<SectionResultResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<SectionResultResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a SectionResult by SectionResultId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int SectionResultId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<SectionResult>(x => x.SectionResultId == SectionResultId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, SectionResultId));// Fetches the  SectionResult by SectionResultId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<SectionResultResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<SectionResultResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  SectionResult record
     public async Task<BaseResponse> Delete(int SectionResultId)
     {
        var existing = await _baseRepository.GetByIdAsync<SectionResult, int>(SectionResultId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, SectionResultId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }

    /// <summary>
    /// Generates a result for a section of an exam.
    /// </summary>
    /// <param name="sectionId">The ID of the section.</param>
    /// <param name="userExamId">The ID of the user's exam attempt.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A response with the generated section result.</returns>
    public async Task<SingleResponse<SectionResultResDto>> GenerateSectionResult(int sectionId, int userExamId, CancellationToken cancellationToken)
    {
        if (sectionId <= 0 || userExamId <= 0)
            throw new ArgumentException("Invalid sectionId or userExamId.");

        var section = await _dbContext.Sections.FirstOrDefaultAsync(s => s.SectionId == sectionId, cancellationToken);

        if (section == null)
        {
            return new SingleResponse<SectionResultResDto>
            {
                Status = HttpStatusCode.NotFound,
                Messages = new List<ResponseMessage> { new ResponseMessage { Message = "Section not found." } }
            };
        }

        var userAnswers = await (from ua in _dbContext.UserAnswers
                                 join uao in _dbContext.UserAnswerOptions on ua.UserAnswerId equals uao.UserAnswerId
                                 join o in _dbContext.Options on uao.OptionId equals o.OptionId
                                 where ua.SectionId == sectionId && ua.UserExamId == userExamId && o.IsCorrect
                                 select ua).Distinct().ToListAsync(cancellationToken);

        var attemptedQuestionsCount = await (from ua in _dbContext.UserAnswers
                                             join uao in _dbContext.UserAnswerOptions on ua.UserAnswerId equals uao.UserAnswerId
                                             where ua.SectionId == sectionId && ua.UserExamId == userExamId
                                             select ua.QuestionId)
                                     .Distinct()
                                     .CountAsync(cancellationToken);


        if (!userAnswers.Any())
        {
            return new SingleResponse<SectionResultResDto>
            {
                Status = HttpStatusCode.NotFound,
                Messages = new List<ResponseMessage> { new ResponseMessage { Message = "No correct answers found for this section." } }
            };
        }

        decimal marksObtained = userAnswers.Sum(ua => (from uao in _dbContext.UserAnswerOptions
                                                       join o in _dbContext.Options on uao.OptionId equals o.OptionId
                                                       where uao.UserAnswerId == ua.UserAnswerId && o.IsCorrect
                                                       select (decimal?)o.Marks).FirstOrDefault() ?? 0);

        var sectionResult = new SectionResult
        {
            SectionId = sectionId,
            UserExamId = userExamId,
            QuestionsAttempted = attemptedQuestionsCount,
            MarksObtained = marksObtained,
            ResultStatus = marksObtained >= section.PassingMarks ? "Pass" : "Fail"
        };

        _dbContext.SectionResults.Add(sectionResult);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var sectionResultDto = new SectionResultResDto
        {
            SectionId = sectionResult.SectionId,
            UserExamId = sectionResult.UserExamId,
            QuestionsAttempted = sectionResult.QuestionsAttempted,
            MarksObtained = sectionResult.MarksObtained,
            ResultStatus = sectionResult.ResultStatus
        };

        return new SingleResponse<SectionResultResDto>
        {
            Data = sectionResultDto,
            Status = HttpStatusCode.Created
        };
    }
}



