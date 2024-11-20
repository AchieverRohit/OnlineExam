public interface IExamService
{
    Task<ListResponse<ExamResDto>> GetAll(GetAllExamReqDto? requestDto);
    Task<SingleResponse<ExamResDto>> Save(AddExamReqDto requestDto);
    Task<SingleResponse<ExamResDto>> Update(UpdateExamReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int ExamId, bool withDetails = false);
    Task<BaseResponse> Delete(int ExamId);
    /// <summary>
    /// Retrieves a list of active exams.
    /// </summary>
    /// <param name = "cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>A ListResponse containing a list of ExamDto objects.</returns>
    Task<ListResponse<ExamDto>> GetActiveExams(CancellationToken cancellationToken);
}