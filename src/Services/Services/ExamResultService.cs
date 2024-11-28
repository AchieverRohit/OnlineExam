
using thinkschool.OnlineExam.Core.Models.ExamResultDtos;
using thinkschool.OnlineExam.Core.Models.SectionResultDtos;

namespace thinkschool.OnlineExam.Services.Services;
public class ExamResultService : IExamResultService
{
          
     private readonly IBaseRepository _baseRepository;// Base repository for database operations
     private readonly IAppDbContext _dbContext; // Database context
     private readonly IMapper _mapper;// Mapper for DTO and entity conversions
     public ExamResultService(IAppDbContext dbContext, IBaseRepository baseRepository, IMapper mapper) 
     {
        _dbContext = dbContext; // Assigning the database context
        _baseRepository = baseRepository;// Assigning the base repository
        _mapper = mapper;// Assigning the mapper

     }
     // Fetches allExamResults or fetchesExamResults with pagination based on requestDto
     public async Task<ListResponse<ExamResultResDto>> GetAll(GetAllExamResultReqDto? requestDto)
     {
        var (response, page) = requestDto == null
        ? (await _baseRepository.GetAllAsync<ExamResult>(), null)
        : await _baseRepository.GetAllWithPaginationAsync<ExamResult, GetAllExamResultReqDto>(requestDto);
        var mappedResponse = _mapper.Map<List<ExamResultResDto>>(response); // Maps entities to response DTOs
        return new ListResponse<ExamResultResDto> { Data = mappedResponse, PageInfo = page! };  // Returns paginated or non-paginated response
     }
     // Adds a new ExamResult to the database
     public async Task<SingleResponse<ExamResultResDto>> Save(AddExamResultReqDto requestDto)
     {
        var entity = _mapper.Map<ExamResult>(requestDto); // Maps the request DTO to the ExamResult entity
        var addedEntity = await _baseRepository.AddAsync(entity);// Adds the entity to the database
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        var mappedResponse = _mapper.Map<ExamResultResDto>(addedEntity); // Maps the added entity to response DTO
        return new SingleResponse<ExamResultResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Updates an existing ExamResult in the database
     public async Task<SingleResponse<ExamResultResDto>> Update(UpdateExamResultReqDto requestDto)
     {
        var existing = await _baseRepository.GetByIdAsync<ExamResult,int>(requestDto.ExamResultId) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, requestDto.ExamResultId)); // Fetches the existing ExamResult by ExamResultId 
        _mapper.Map(requestDto, existing);  // Maps the update DTO to the existing entity
        _baseRepository.Update(existing); // Updates the entity in the database
        await _dbContext.SaveChangesAsync();  // Saves changes to the database
        var mappedResponse = _mapper.Map<ExamResultResDto>(existing); // Maps the updated entity to response DTO
        return new SingleResponse<ExamResultResDto>{ Data = mappedResponse }; // Returns the response
     }
     // Fetches a ExamResult by ExamResultId with optional details
     public async Task<SingleResponse<dynamic>> GetById(int ExamResultId, bool withDetails = false)
     {
        var response = await _baseRepository.GetFirstAsync<ExamResult>(x => x.ExamResultId == ExamResultId ) 
            ?? throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ExamResultId));// Fetches the  ExamResult by ExamResultId or throws a NotFoundException if not found

        var records = withDetails
            ? _mapper.Map<ExamResultResDetailDto>(response)// Maps to detailed response DTO if withDetails is true
            : _mapper.Map<ExamResultResDto>(response); // Maps to simple response DTO if withDetails is false
        return new SingleResponse<dynamic> { Data = records }; // Returns the response
     }
     // deleted  ExamResult record
     public async Task<BaseResponse> Delete(int ExamResultId)
     {
        var existing = await _baseRepository.GetByIdAsync<ExamResult, int>(ExamResultId)
            ??  throw new NotFoundException(string.Format(ConstantsValues.NoRecord, ExamResultId)); 
        // This record will be permanently deleted from the database and cannot be recovered.        _baseRepository.Delete(existing);             
        await _dbContext.SaveChangesAsync(); // Saves changes to the database
        return new BaseResponse(); // Returns a base response
     }

    /// <summary>
    /// Calculates and creates an exam result for the given user exam ID.
    /// </summary>
    /// <param name="userExamId">The ID of the user exam.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>SingleResponse containing the ExamResultDto or an error message.</returns>
    /// <exception cref="ArgumentException">Thrown when userExamId is less than or equal to zero.</exception>
    public async Task<SingleResponse<ExamResultShortDto>> CalculateAndCreateExamResult(int userExamId, CancellationToken cancellationToken)
    {
        if (userExamId <= 0)
        {
            throw new ArgumentException("Invalid user exam ID.", nameof(userExamId));
        }

        var sectionResults = await _dbContext.SectionResults
                             .Where(sr => sr.UserExamId == userExamId)
                             .ToListAsync(cancellationToken);

        if (!sectionResults.Any())
        {
            return new SingleResponse<ExamResultShortDto>
            {
                Status = HttpStatusCode.NotFound,
                Messages = new List<ResponseMessage> { new ResponseMessage { Message = "No section results found for the given UserExamID." } }
            };
        }

        var totalObtainedMarks = sectionResults.Sum(sr => sr.MarksObtained);
        var resultStatus = sectionResults.All(sr => sr.ResultStatus == "Pass") ? "Pass" : "Fail";


        //var SectionResultDtos = new List<SectionResultResDto>();

        //foreach (var sectionResult in sectionResults) {
        //    SectionResultDtos.Add(
        //    _mapper.Map<SectionResultResDto>(sectionResult)
        //    );
        //}

        var examResult = new ExamResult
        {
            UserExamId = userExamId,
            TotalObtainedMarks = totalObtainedMarks,
            ResultStatus = resultStatus,
            CreatedBy = "System",
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow,
        };

        try
        {
            _dbContext.ExamResults.Add(examResult);

            var userExam = await _dbContext.UserExams
                                 .Where(ue => ue.UserExamId == userExamId)
                                 .FirstOrDefaultAsync(cancellationToken);

            if (userExam != null)
            {
                userExam.ExamStatus = "Completed";
                userExam.FinishedOn = DateTime.UtcNow;
                userExam.NoOfAttempt += 1;
                userExam.UpdatedOn = DateTime.UtcNow;
                userExam.TotalMarks = totalObtainedMarks;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // Log the exception (assumed to be implemented)
            throw new InvalidOperationException("An error occurred while saving the exam result.", ex);
        }

        //var examResultDto = new ExamResultDetailsResponseDto
        //{
        //    ExamResultId = examResult.ExamResultId,
        //    TotalObtainedMarks = examResult.TotalObtainedMarks,
        //    ResultStatus = resultStatus,
        //    CreatedBy = examResult.CreatedBy,
        //    CreatedOn = examResult.CreatedOn,
        //    UpdatedOn = examResult.UpdatedOn,
        //    SectionResults = SectionResultDtos
        //};

        var examResultDto = new ExamResultShortDto
        {
            UserExamId = userExamId
        };

        return new SingleResponse<ExamResultShortDto>
        {
            Data = examResultDto,
            Status = HttpStatusCode.Created
        };
    }

    /// <summary>
    /// Retrieves exam results for a given exam ID.
    /// </summary>
    /// <param name="examId">The ID of the exam.</param>
    /// <param name="cancellationToken">CancellationToken for async operation.</param>
    /// <returns>A ListResponse containing a list of ExamResultDto.</returns>
    /// <exception cref="ArgumentException">Thrown when examId is less than or equal to zero.</exception>
    public async Task<ListResponse<ExamResultViewModel>> GetExamResultsByExamId(int examId, CancellationToken cancellationToken)
    {
        if (examId <= 0) throw new ArgumentException("Exam ID must be greater than zero.", nameof(examId));

        try
        {
            var examResults = await _dbContext.UserExams
                .Where(ue => ue.ExamId == examId)
                .Join(_dbContext.ApplicationUsers, ue => ue.UserId, au => au.Id, (ue, au) => new { ue, au })
                .Join(_dbContext.ExamResults, combined => combined.ue.UserExamId, er => er.UserExamId, (combined, er) => new ExamResultViewModel
                {
                    UserExamId = combined.ue.UserExamId,
                    Name = combined.au.FirstName + " " + combined.au.LastName,
                    Percentage = (combined.ue.TotalMarks != 0) ? er.TotalObtainedMarks / (decimal)combined.ue.TotalMarks * 100 : 0,
                    ResultStatus = er.ResultStatus
                })
                .ToListAsync(cancellationToken);

            return new ListResponse<ExamResultViewModel> { Data = examResults };
        }
        catch (Exception ex)
        {
            // Log the exception (implement logging according to your logging framework)
            throw;
        }
    }

    /// <summary>
    /// Retrieves the exam result details for a specific user exam.
    /// </summary>
    /// <param name="userExamId">The ID of the user exam.</param>
    /// <param name="cancellationToken">Token for canceling the operation.</param>
    /// <returns>Returns the exam result details.</returns>
    /// <exception cref="ArgumentException">Thrown when userExamId is less than or equal to 0.</exception>
    public async Task<SingleResponse<ExamResultDetailsViewModel>> GetExamResultByUserExamId(int userExamId, CancellationToken cancellationToken)
    {
        if (userExamId <= 0)
        {
            throw new ArgumentException("User exam ID must be greater than 0.", nameof(userExamId));
        }

        var examResult = await _dbContext.ExamResults
            .Where(er => er.UserExamId == userExamId)
            .Select(er => new ExamResultDetailsViewModel
            {
                ExamResultId = er.ExamResultId,
                TotalScore = er.TotalObtainedMarks,
                ResultStatus = er.ResultStatus,
                SectionResults = _dbContext.SectionResults
                    .Where(sr => sr.UserExamId == userExamId)
                    .Join(_dbContext.Sections,
                          sr => sr.SectionId,
                          s => s.SectionId,
                          (sr, s) => new SectionResultDetailsViewModel
                          {
                              SectionId = sr.SectionId,
                              SectionName = s.Title,
                              QuestionsAttempted = sr.QuestionsAttempted,
                              SectionTotalMarks = s.TotalMarks,
                              ObtainedMarks = sr.MarksObtained,
                              SectionResultStatus = sr.ResultStatus
                          })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (examResult == null)
        {
            return new SingleResponse<ExamResultDetailsViewModel>
            {
                Status = HttpStatusCode.NotFound,
                Messages = new List<ResponseMessage> { new ResponseMessage { Message = "Exam result not found for the given UserExamId." } }
            };
        }

        return new SingleResponse<ExamResultDetailsViewModel> { Data = examResult };
    }
}



