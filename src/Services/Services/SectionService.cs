
namespace thinkschool.OnlineExam.Services.Services;
public class SectionService : ISectionService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public SectionService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allSections or fetchesSections with pagination based on requestDto
     public async Task<ListResponse<SectionResDto>> GetAll(GetAllSectionReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<Section>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<Section, GetAllSectionReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<SectionResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<SectionResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new Section to the database
     public async Task<SingleResponse<SectionResDto>> Save(AddSectionReqDto requestDto)
     {
        var entity = _mapper.Map<Section>(requestDto); // Maps the request DTO to the Section entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<SectionResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<SectionResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing Section in the database
     public async Task<SingleResponse<SectionResDto>> Update(UpdateSectionReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<Section,int>(requestDto.SectionId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.SectionId)); // Fetches the existing Section by SectionId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<SectionResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<SectionResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a Section by SectionId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int SectionId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<Section>(x => x.SectionId == SectionId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, SectionId));// Fetches the  Section by SectionId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<SectionResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<SectionResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  Section record
     public async Task<BaseResponse> Delete(int SectionId)
     {
        var existing = await _baseRepository.GetByIdAsync<Section, int>(SectionId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, SectionId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }

    /// <summary>
    /// Retrieves a list of SectionDto objects by ExamId.
    /// </summary>
    /// <param name="examId">The exam identification number.</param>
    /// <param name="cancellationToken">Token to cancel operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a ListResponse of SectionDto.</returns>
    /// <exception cref="ArgumentException">Thrown when examId is less than or equal to zero.</exception>
    public async Task<ListResponse<SectionResDto>> GetSectionsByExamId(int examId, CancellationToken cancellationToken)
    {
        if (examId <= 0)
        {
            throw new ArgumentException("ExamId must be greater than zero.", nameof(examId));
        }

        try
        {
            var sections = await _dbContext.Sections
                .Where(s => s.ExamId == examId)
                .Select(s => new SectionResDto
                {
                    SectionId = s.SectionId,
                    Title = s.Title,
                    TotalQuestions = s.TotalQuestions,
                    TotalMarks = s.TotalMarks,
                    PassingMarks = s.PassingMarks,
                    WeightagePercentage = s.WeightagePercentage,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn,
                    UpdatedOn = s.UpdatedOn
                }).ToListAsync(cancellationToken);

            return new ListResponse<SectionResDto> { Data = sections };
        }
        catch (Exception ex)
        {
            // Log exception here or handle accordingly
            throw new ApplicationException("An error occurred while retrieving sections.", ex);
        }
    }

    /// <summary>
    /// Retrieves section details including associated questions and options based on the section ID.
    /// </summary>
    /// <param name="sectionId">The ID of the section to retrieve details for.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A SingleResponse containing the section details</returns>
    /// <exception cref="ArgumentException">Thrown when sectionId is less than or equal to zero.</exception>
    public async Task<SingleResponse<SectionDetailsResponseDto>> GetSectionDetails(int sectionId, CancellationToken cancellationToken)
    {
        if (sectionId <= 0)
        {
            throw new ArgumentException("SectionId must be greater than zero.", nameof(sectionId));
        }

        try
        {
            var sectionDetails = await (from s in _dbContext.Sections
                                        where s.SectionId == sectionId
                                        select new SectionDetailsResponseDto
                                        {
                                            SectionId = s.SectionId,
                                            Title = s.Title,
                                            TotalQuestions = s.TotalQuestions,
                                            TotalMarks = s.TotalMarks,
                                            PassingMarks = s.PassingMarks,
                                            WeightagePercentage = s.WeightagePercentage,
                                            Questions = _dbContext.Questions
                                                    .Where(q => q.SectionId == s.SectionId)
                                                    .Select(q => new QuestionDetailsResponseDto
                                                    {
                                                        QuestionId = q.QuestionId,
                                                        QuestionText = q.QuestionText,
                                                        IsMedia = q.IsMedia,
                                                        MediaType = q.MediaType,
                                                        MediaURL = q.MediaURL,
                                                        IsMultipleChoice = q.IsMultipleChoice,
                                                        IsFromQuestionBank = q.IsFromQuestionBank,
                                                        QuestionMaxMarks = q.QuestionMaxMarks,
                                                        Options = _dbContext.Options
                                                                .Where(o => o.QuestionId == q.QuestionId)
                                                                .Select(o => new OptionResDto
                                                                {
                                                                    OptionId = o.OptionId,
                                                                    OptionText = o.OptionText,
                                                                    IsCorrect = o.IsCorrect,
                                                                    Marks = o.Marks
                                                                }).ToList()
                                                    }).ToList()
                                        }).FirstOrDefaultAsync(cancellationToken);

            return new SingleResponse<SectionDetailsResponseDto> { Data = sectionDetails };
        }
        catch (Exception ex)
        {
            // Log exception
            throw new ApplicationException("An error occurred while retrieving section details.", ex);
        }
    }
}



