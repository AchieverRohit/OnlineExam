using thinkschool.OnlineExam.Core.Models.ExamDtos;

public interface IExamService
{
    Task<ListResponse<ExamResDto>> GetAll(GetAllExamReqDto? requestDto);
    Task<SingleResponse<ExamResDto>> Save(AddExamReqDto requestDto);
    Task<SingleResponse<ExamResDto>> Update(UpdateExamReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int ExamId, bool withDetails = false);
    Task<BaseResponse> Delete(int ExamId);
    Task<ListResponse<ExamDto>> GetExamsByTeacherId(string teacherId, CancellationToken cancellationToken);

    Task<SingleResponse<ExamDto>> UpdateExam(ExamDto examDto, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves all active exams.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A list response with active exams.</returns>
    Task<ListResponse<ExamResDto>> GetActiveExams(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves exam details by examId.
    /// </summary>
    /// <param name="examId">The ID of the exam to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A SingleResponse containing the exam details.</returns>
    Task<SingleResponse<ExamDetailsResponseDto>> GetExamWithDetails(int examId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves exam data for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve exam data for.</param>
    /// <param name="cancellationToken">Cancellation token for async operation.</param>
    /// <returns>A list response containing the exam data view model.</returns>
    Task<ListResponse<GetExamDataViewModel>> GetExamDataByUserId(string userId, CancellationToken cancellationToken);
}