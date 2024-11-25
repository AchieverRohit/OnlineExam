
using thinkschool.OnlineExam.Core.Models.UserAnswerDtos;

namespace thinkschool.OnlineExam.Services.IServices;
public interface IUserAnswerService
{
 
    Task<ListResponse<UserAnswerResDto>> GetAll(GetAllUserAnswerReqDto? requestDto);
    Task<SingleResponse<UserAnswerResDto>> Save(AddUserAnswerReqDto requestDto);
    Task<SingleResponse<UserAnswerResDto>> Update(UpdateUserAnswerReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  UserAnswerId, bool withDetails = false);
    Task<BaseResponse> Delete(int  UserAnswerId);
    Task<SingleResponse<string>> SubmitAnswer(SubmitAnswerDto submitAnswerDto, CancellationToken cancellationToken);
}



