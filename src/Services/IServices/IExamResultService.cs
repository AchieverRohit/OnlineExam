
namespace thinkschool.OnlineExam.Services.IServices;
public interface IExamResultService
{
 
    Task<ListResponse<ExamResultResDto>> GetAll(GetAllExamResultReqDto? requestDto);
    Task<SingleResponse<ExamResultResDto>> Save(AddExamResultReqDto requestDto);
    Task<SingleResponse<ExamResultResDto>> Update(UpdateExamResultReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  ExamResultId, bool withDetails = false);
    Task<BaseResponse> Delete(int  ExamResultId);
  
}



