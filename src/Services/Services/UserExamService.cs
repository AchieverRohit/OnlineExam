
namespace thinkschool.OnlineExam.Services.Services;
public class UserExamService : IUserExamService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public UserExamService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allUserExams or fetchesUserExams with pagination based on requestDto
     public async Task<ListResponse<UserExamResDto>> GetAll(GetAllUserExamReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<UserExam>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<UserExam, GetAllUserExamReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<UserExamResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<UserExamResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new UserExam to the database
     public async Task<SingleResponse<UserExamResDto>> Save(AddUserExamReqDto requestDto)
     {
        var entity = _mapper.Map<UserExam>(requestDto); // Maps the request DTO to the UserExam entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<UserExamResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<UserExamResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing UserExam in the database
     public async Task<SingleResponse<UserExamResDto>> Update(UpdateUserExamReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<UserExam,int>(requestDto.UserExamId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.UserExamId)); // Fetches the existing UserExam by UserExamId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<UserExamResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<UserExamResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a UserExam by UserExamId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int UserExamId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<UserExam>(x => x.UserExamId == UserExamId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, UserExamId));// Fetches the  UserExam by UserExamId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<UserExamResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<UserExamResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  UserExam record
     public async Task<BaseResponse> Delete(int UserExamId)
     {
        var existing = await _baseRepository.GetByIdAsync<UserExam, int>(UserExamId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, UserExamId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }

    /// <summary>
    /// Retrieves a list of user exams and their associated results by exam ID.
    /// </summary>
    /// <param name="examId">The ID of the exam for which user exams are to be retrieved.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>A list of user exams along with their results.</returns>
    /// <exception cref="ArgumentException">Thrown when examId is less than or equal to zero.</exception>
    public async Task<ListResponse<UserExamWithResultDto>> GetUserExamsByExamId(int examId, CancellationToken cancellationToken)
    {
        if (examId <= 0) throw new ArgumentException("Invalid exam Id", nameof(examId));

        var userExams = await _dbContext.UserExams
            .Where(ue => ue.ExamId == examId)
            .GroupJoin(_dbContext.ExamResults,
                      ue => ue.UserExamId,
                      er => er.UserExamId,
                      (ue, userExamResults) => new { ue, userExamResults })
            .SelectMany(temp => temp.userExamResults.DefaultIfEmpty(),
                        (temp, examResult) => new UserExamWithResultDto
                        {
                            UserExamId = temp.ue.UserExamId,
                            UserId = temp.ue.UserId,
                            ExamId = temp.ue.ExamId,
                            StartedOn = temp.ue.StartedOn,
                            FinishedOn = temp.ue.FinishedOn,
                            ExamStatus = temp.ue.ExamStatus,
                            TotalMarks = temp.ue.TotalMarks,
                            IsAutoSubmitted = temp.ue.IsAutoSubmitted,
                            NoOfAttempt = temp.ue.NoOfAttempt,
                            CreatedBy = temp.ue.CreatedBy,
                            CreatedOn = temp.ue.CreatedOn,
                            UpdatedOn = temp.ue.UpdatedOn,
                            ExamResult = examResult == null ? null : new ExamResultDto
                            {
                                ExamResultId = examResult.ExamResultId,
                                TotalObtainedMarks = examResult.TotalObtainedMarks,
                                ResultStatus = examResult.ResultStatus,
                                CreatedBy = examResult.CreatedBy,
                                CreatedOn = examResult.CreatedOn,
                                UpdatedOn = examResult.UpdatedOn
                            }
                        }).ToListAsync(cancellationToken);

        return new ListResponse<UserExamWithResultDto> { Data = userExams };
    }

    /// <summary>
    /// Retrieves the details of a user's exam based on the provided UserExamId.
    /// </summary>
    /// <param name="userExamId">The ID of the user exam.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>A DTO containing the user exam details.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the UserExamId is not valid.</exception>
    public async Task<SingleResponse<UserExamDetailsResponseDto>> GetUserExamDetails(int userExamId, CancellationToken cancellationToken)
    {
        if (userExamId <= 0)
        {
            throw new ArgumentNullException(nameof(userExamId), "UserExamId must be greater than zero.");
        }

        try
        {
            var userExamDetails = await _dbContext.UserExams
                .Where(ue => ue.UserExamId == userExamId)
                .Join(_dbContext.ExamResults,
                      ue => ue.UserExamId,
                      er => er.UserExamId,
                      (ue, er) => new { ue, er })
                .Join(_dbContext.Exams,
                      temp => temp.ue.ExamId,
                      e => e.ExamId,
                      (temp, e) => new UserExamDetailsResponseDto
                      {
                          TotalMarks = temp.ue.TotalMarks,
                          ExamStatus = temp.ue.ExamStatus,
                          NoOfAttempt = temp.ue.NoOfAttempt,
                          RsultDate = temp.er.CreatedOn,
                          TotalObtainedMarks = temp.er.TotalObtainedMarks,
                          ResultStatus = temp.er.ResultStatus,
                          PassingMarks = e.PassingMarks
                      })
                .FirstOrDefaultAsync(cancellationToken);

            return new SingleResponse<UserExamDetailsResponseDto> { Data = userExamDetails };
        }
        catch (Exception ex)
        {
            // Log the exception
            throw;
        }
    }


    /// <summary>
    /// Retrieves a list of user exam reports.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>A list response containing UserExamReportDto objects.</returns>
    public async Task<ListResponse<UserExamReportDto>> GetUserExamReport(CancellationToken cancellationToken)
    {
        if (cancellationToken == null)
        {
            throw new ArgumentNullException(nameof(cancellationToken));
        }

        try
        {
            var reportData = await _dbContext.UserExams
                    .Join(_dbContext.Exams,
                          ue => ue.ExamId,
                          e => e.ExamId,
                          (ue, e) => new { ue, e })
                    .Join(_dbContext.ApplicationUsers,
                          combined => combined.ue.UserId,
                          user => user.Id,
                          (combined, user) => new { combined.ue, combined.e, user })
                    .Where(x => x.ue.FinishedOn.HasValue &&
                                (x.ue.FinishedOn.Value - x.ue.StartedOn).TotalMinutes < x.e.Duration * 0.8)
                    .Select(x => new UserExamReportDto
                    {
                        UserExamId = x.ue.UserExamId,
                        UserId = x.ue.UserId,
                        ExamId = x.ue.ExamId,
                        ExamStatus = x.ue.ExamStatus,
                        TotalMarks = x.ue.TotalMarks,
                        NoOfAttempt = x.ue.NoOfAttempt,
                        Title = x.e.Title,
                        Duration = x.e.Duration,
                        FirstName = x.user.FirstName,
                        LastName = x.user.LastName,
                        Email = x.user.Email
                    }).ToListAsync(cancellationToken);


            return new ListResponse<UserExamReportDto> { Data = reportData };
        }
        catch (Exception ex)
        {
            // Log exception
            throw new ApplicationException("An error occurred while generating the user exam report.", ex);
        }
    }

}




