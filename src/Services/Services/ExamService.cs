public class ExamService : IExamService
{
    private readonly IBaseRepository _baseRepository; // Base repository for database operations
    private readonly IAppDbContext _dbContext; // Database context
    private readonly IMapper _mapper; // Mapper for DTO and entity conversions
    public ExamService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper)
    {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository; // Assigning the base repository
        _mapper = mapper; // Assigning the mapper
    }

    // Fetches allExams or fetchesExams with pagination based on requestDto
    public async Task<ListResponse<ExamResDto>> GetAll(GetAllExamReqDto? requestDto)
    {
        var(response, page) = requestDto == null ? (await _baseRepository.GetAllAsync<Exam>(), null) : await _baseRepository.GetAllWithPaginationAsync<Exam, GetAllExamReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<ExamResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<ExamResDto>
        {
            Data = mappedResponse,
            PageInfo = page!
        }; // Returns paginated or non-paginated response
    }

    // Adds a new Exam to the database
    public async Task<SingleResponse<ExamResDto>> Save(AddExamReqDto requestDto)
    {
        var entity = _mapper.Map<Exam>(requestDto); // Maps the request DTO to the Exam entity
        var addedEntity = await _baseRepository.AddAsync(entity); // Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<ExamResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<ExamResDto>
        {
            Data = mappedResponse
        }; // Returns the response
    }

    // Updates an existing Exam in the database
    public async Task<SingleResponse<ExamResDto>> Update(UpdateExamReqDto requestDto)
    {
        var existing = await _baseRepository.GetByIdAsync<Exam, int>(requestDto.ExamId) ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.ExamId)); // Fetches the existing Exam by ExamId 
        _mapper.Map(requestDto, existing); // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<ExamResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<ExamResDto>
        {
            Data = mappedResponse
        }; // Returns the response
    }

    // Fetches a Exam by ExamId with optional details
    public async Task<SingleResponse<dynamic>> GetById(int ExamId, bool withDetails = false)
    {
        var response = await _baseRepository.GetFirstAsync<Exam>(x => x.ExamId == ExamId) ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ExamId)); // Fetches the  Exam by ExamId or throws a NotFoundException if not found
        var records = withDetails ? _mapper.Map<ExamResDetailDto>(response) // Maps to detailed response DTO if withDetails is true
 : _mapper.Map<ExamResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic>
        {
            Data = records
        }; // Returns the response
    }

    // deleted  Exam record
    public async Task<BaseResponse> Delete(int ExamId)
    {
        var existing = await _baseRepository.GetByIdAsync<Exam, int>(ExamId) ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ExamId));
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
    }

    /// <summary>
    /// Retrieves a list of active exams.
    /// </summary>
    /// <param name = "cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A ListResponse containing a list of ExamDto objects.</returns>
    /// <exception cref = "Exception">Thrown when an error occurs in database access.</exception>
    public async Task<ListResponse<ExamDto>> GetActiveExams(CancellationToken cancellationToken)
    {
        try
        {
            var activeExams = await _dbContext.Exams.Where(e => e.IsActive).Select(e => new ExamDto { ExamId = e.ExamId, Title = e.Title, Description = e.Description, StartDate = e.StartDate, EndDate = e.EndDate, Duration = e.Duration, TotalQuestions = e.TotalQuestions, TotalMarks = e.TotalMarks, PassingMarks = e.PassingMarks, IsRandomized = e.IsRandomized, IsActive = e.IsActive, CreatedBy = e.CreatedBy, CreatedOn = e.CreatedOn, UpdatedOn = e.UpdatedOn }).ToListAsync(cancellationToken);
            return new ListResponse<ExamDto>
            {
                Data = activeExams
            };
        }
        catch (Exception ex)
        {
            // Log exception
            throw new Exception("An error occurred while retrieving active exams.", ex);
        }
    }

    /// <summary>
    /// Retrieves exams created by a specific teacher.
    /// </summary>
    /// <param name="teacherId">The ID of the teacher whose exams are to be retrieved.</param>
    /// <param name="cancellationToken">The cancellation token for task cancellation.</param>
    /// <returns>A list response containing exam DTOs.</returns>
    /// <exception cref="ArgumentNullException">Thrown when teacherId is null or empty.</exception>
    public async Task<ListResponse<ExamDto>> GetExamsByTeacherId(string teacherId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(teacherId))
        {
            throw new ArgumentNullException(nameof(teacherId), "Teacher ID cannot be null or empty.");
        }

        var exams = await _dbContext.Exams
            .Where(e => e.CreatedBy == teacherId)
            .Select(e => new ExamDto
            {
                ExamId = e.ExamId,
                Title = e.Title,
                Description = e.Description,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                Duration = e.Duration,
                TotalQuestions = e.TotalQuestions,
                TotalMarks = e.TotalMarks,
                PassingMarks = e.PassingMarks,
                IsRandomized = e.IsRandomized,
                IsActive = e.IsActive,
                CreatedBy = e.CreatedBy,
                CreatedOn = e.CreatedOn,
                UpdatedOn = e.UpdatedOn
            })
            .ToListAsync(cancellationToken);

        return new ListResponse<ExamDto> { Data = exams };
    }
}