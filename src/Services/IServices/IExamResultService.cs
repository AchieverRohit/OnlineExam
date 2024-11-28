
using thinkschool.OnlineExam.Core.Models.ExamResultDtos;

namespace thinkschool.OnlineExam.Services.IServices;
public interface IExamResultService
{
 
    Task<ListResponse<ExamResultResDto>> GetAll(GetAllExamResultReqDto? requestDto);
    Task<SingleResponse<ExamResultResDto>> Save(AddExamResultReqDto requestDto);
    Task<SingleResponse<ExamResultResDto>> Update(UpdateExamResultReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  ExamResultId, bool withDetails = false);
    Task<BaseResponse> Delete(int  ExamResultId);

    Task<SingleResponse<ExamResultShortDto>> CalculateAndCreateExamResult(int userExamId, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves exam results for a given exam ID.
    /// </summary>
    /// <param name="examId">The ID of the exam.</param>
    /// <param name="cancellationToken">CancellationToken for async operation.</param>
    /// <returns>A ListResponse containing a list of ExamResultDto.</returns>
    Task<ListResponse<ExamResultViewModel>> GetExamResultsByExamId(int examId, CancellationToken cancellationToken);
}



