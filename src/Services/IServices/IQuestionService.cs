
namespace thinkschool.OnlineExam.Services.IServices;
public interface IQuestionService
{
 
    Task<ListResponse<QuestionResDto>> GetAll(GetAllQuestionReqDto? requestDto);
    Task<SingleResponse<QuestionResDto>> Save(AddQuestionReqDto requestDto);
    Task<SingleResponse<QuestionResDto>> Update(UpdateQuestionReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  QuestionId, bool withDetails = false);
    Task<BaseResponse> Delete(int  QuestionId);
    Task<SingleResponse<QuestionDto>> AddQuestionWithOptions(QuestionDto questionDto, CancellationToken cancellationToken);


}



