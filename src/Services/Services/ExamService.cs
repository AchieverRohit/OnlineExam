using thinkschool.OnlineExam.Core.Models.ExamDtos;
using thinkschool.OnlineExam.Core.Validations;

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
    //public async Task<ListResponse<ExamDto>> GetActiveExams(CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        var activeExams = await _dbContext.Exams.Where(e => e.IsActive).Select(e => new ExamDto { ExamId = e.ExamId, Title = e.Title, Description = e.Description, StartDate = e.StartDate, EndDate = e.EndDate, Duration = e.Duration, TotalQuestions = e.TotalQuestions, TotalMarks = e.TotalMarks, PassingMarks = e.PassingMarks, IsRandomized = e.IsRandomized, IsActive = e.IsActive, CreatedBy = e.CreatedBy, CreatedOn = e.CreatedOn, UpdatedOn = e.UpdatedOn }).ToListAsync(cancellationToken);
    //        return new ListResponse<ExamDto>
    //        {
    //            Data = activeExams
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log exception
    //        throw new Exception("An error occurred while retrieving active exams.", ex);
    //    }
    //}

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

    /// <summary>
    /// Updates an existing exam in the database.
    /// </summary>
    /// <param name="examDto">The exam data transfer object containing updated exam details.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the request.</param>
    /// <returns>A Task that represents the asynchronous operation, containing a SingleResponse with the updated ExamDto.</returns>
    public async Task<SingleResponse<ExamDto>> UpdateExam(ExamDto examDto, CancellationToken cancellationToken)
    {
        if (examDto == null) throw new ArgumentNullException(nameof(examDto));

        var validator = new ExamDtoValidator();
        var validationResult = validator.Validate(examDto);

        if (!validationResult.IsValid)
        {
            return new SingleResponse<ExamDto>
            {
                Status = HttpStatusCode.BadRequest,
                Messages = validationResult.Errors.Select(e => new ResponseMessage { Message = e.ErrorMessage }).ToList()
            };
        }

        var exam = await _dbContext.Exams.FirstOrDefaultAsync(e => e.ExamId == examDto.ExamId, cancellationToken);
        if (exam == null)
        {
            return new SingleResponse<ExamDto>
            {
                Status = HttpStatusCode.NotFound,
                Messages = new List<ResponseMessage> { new ResponseMessage { Message = "Exam not found." } }
            };
        }


        exam.Title = examDto.Title;
        exam.Description = examDto.Description;
        exam.StartDate = examDto.StartDate;
        exam.EndDate = examDto.EndDate;
        exam.Duration = examDto.Duration;
        exam.TotalQuestions = examDto.TotalQuestions;
        exam.TotalMarks = examDto.TotalMarks;
        exam.PassingMarks = examDto.PassingMarks;
        exam.IsRandomized = examDto.IsRandomized;
        exam.IsActive = examDto.IsActive;
        exam.CreatedBy = examDto.CreatedBy;
        exam.CreatedOn = examDto.CreatedOn;
        exam.UpdatedOn = DateTime.UtcNow;

        try
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            // Log exception here
            return new SingleResponse<ExamDto>
            {
                Status = HttpStatusCode.InternalServerError,
                Messages = new List<ResponseMessage> { new ResponseMessage { Message = "An error occurred while updating the exam." } }
            };
        }

        return new SingleResponse<ExamDto>
        {
            Data = examDto,
            Status = HttpStatusCode.OK
        };
    }

    /// <summary>

    /// Retrieves all active exams.

    /// </summary>

    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>

    /// <returns>A list response with active exams.</returns>

    public async Task<ListResponse<ExamResDto>> GetActiveExams(CancellationToken cancellationToken)

    {

        if (cancellationToken == null) throw new ArgumentNullException(nameof(cancellationToken));

        try

        {

            var activeExams = await _dbContext.Exams

                .Where(e => e.IsActive == true)

                .AsNoTracking()

                .ToListAsync(cancellationToken);

            var mappedResponse = _mapper.Map<List<ExamResDto>>(activeExams);

            return new ListResponse<ExamResDto> { Data = mappedResponse };

        }

        catch (Exception ex)

        {

            // Log the exception

            // logger.LogError(ex, "Error occurred while retrieving active exams.");

            throw;

        }

    }

    /// <summary>

    /// Retrieves exam details by examId, including sections, questions and options.

    /// </summary>

    /// <param name="examId">The ID of the exam to retrieve.</param>

    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>

    /// <returns>A SingleResponse containing the exam details or an error message if not found.</returns>

    /// <exception cref="ArgumentException">Thrown when examId is less than or equal to zero.</exception>

    public async Task<SingleResponse<ExamDetailsResponseDto>> GetExamWithDetails(int examId, CancellationToken cancellationToken)

    {

        if (examId <= 0)

            throw new ArgumentException("Exam ID must be greater than zero.", nameof(examId));

        var examData = _dbContext.Exams
                    .Select(e => new ExamDetailsResponseDto
                    {
                        ExamId = examId,
                        CreatedBy = e.CreatedBy,
                        UpdatedOn = e.UpdatedOn,
                        CreatedOn = e.CreatedOn,
                        Description = e.Description,
                        Duration = e.Duration,
                        EndDate = e.EndDate,
                        IsActive = e.IsActive,
                        IsRandomized = e.IsRandomized,
                        PassingMarks = e.PassingMarks,
                        StartDate = e.StartDate,
                        Title = e.Title,
                        TotalMarks = e.TotalMarks,
                        TotalQuestions = e.TotalQuestions,
                        SectionExams = e.SectionExams.Select(s => new SectionDetailsResponseDto
                        {
                            SectionId = s.SectionId,
                            CreatedBy = s.CreatedBy,
                            UpdatedOn = s.UpdatedOn,
                            TotalQuestions = s.TotalQuestions,
                            TotalMarks = s.TotalMarks,
                            PassingMarks = s.PassingMarks,
                            CreatedOn = s.CreatedOn,
                            WeightagePercentage = s.WeightagePercentage,
                            ExamId = s.ExamId,
                            Questions = s.QuestionSections.Select(q => new QuestionDetailsResponseDto
                            {
                                CreatedBy = q.CreatedBy,
                                SectionId = q.SectionId,
                                IsFromQuestionBank = q.IsFromQuestionBank,
                                IsMedia = q.IsMedia,
                                IsMultipleChoice = q.IsMultipleChoice,
                                MediaType = q.MediaType,
                                MediaURL = q.MediaURL,
                                QuestionMaxMarks = q.QuestionMaxMarks,
                                QuestionText = q.QuestionText,
                                Options = q.OptionQuestions.Select(o => new OptionResDto
                                {
                                    OptionId = o.OptionId,
                                    QuestionId = o.QuestionId,
                                    Marks = o.Marks,
                                    OptionText = o.OptionText,
                                    IsCorrect = o.IsCorrect
                                }).ToList()
                            }).ToList()
                        }).ToList()
                    })
                    .FirstOrDefault();


        if (examData == null)

        {

            return new SingleResponse<ExamDetailsResponseDto>

            {

                Status = HttpStatusCode.NotFound,

                Messages = new List<ResponseMessage> { new ResponseMessage { Message = "Exam not found." } }

            };

        }

        var mappedResponse = _mapper.Map<ExamDetailsResponseDto>(examData);

        return new SingleResponse<ExamDetailsResponseDto> { Data = mappedResponse };

    }

    /// <summary>
    /// Retrieves exam data for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve exam data for.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>A list response containing the exam data view model.</returns>
    /// <exception cref="ArgumentNullException">Thrown when userId is null or white space.</exception>
    public async Task<ListResponse<GetExamDataViewModel>> GetExamDataByUserId(string userId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentNullException(nameof(userId), "User ID cannot be null or empty.");

        try
        {
            var currentDate = DateTime.Now;

            var examDataList = await _dbContext.Exams
                .Where(e => e.CreatedBy == userId)
                .Select(exam => new GetExamDataViewModel
                {
                    ExamId = exam.ExamId,
                    ExamName = exam.Title,
                    ExamStatus = exam.IsActive && currentDate >= exam.StartDate && currentDate <= exam.EndDate ? "Active" : "Inactive",
                    TotalStudents = _dbContext.UserExams.Count(ue => ue.ExamId == exam.ExamId),
                    PassedStudents = _dbContext.UserExams.Join(
                        _dbContext.ExamResults,
                        ue => ue.UserExamId,
                        er => er.UserExamId,
                        (ue, er) => new { ue, er })
                        .Count(x => x.er.ResultStatus == "Pass" && x.ue.ExamId == exam.ExamId),
                })
                .ToListAsync(cancellationToken);

            foreach (var examData in examDataList)
            {
                examData.OverallPassStudentsPercentage = examData.TotalStudents > 0 ?
                    (decimal)examData.PassedStudents / examData.TotalStudents * 100 : 0;
            }

            return new ListResponse<GetExamDataViewModel> { Data = examDataList };
        }
        catch (Exception ex)
        {
            // Log or handle exception as needed
            throw new Exception("An error occurred while fetching exam data.", ex);
        }
    }

}