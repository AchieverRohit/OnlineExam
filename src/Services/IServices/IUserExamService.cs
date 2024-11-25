
namespace thinkschool.OnlineExam.Services.IServices;
public interface IUserExamService
{
 
    Task<ListResponse<UserExamResDto>> GetAll(GetAllUserExamReqDto? requestDto);
    Task<SingleResponse<UserExamResDto>> Save(AddUserExamReqDto requestDto);
    Task<SingleResponse<UserExamResDto>> Update(UpdateUserExamReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  UserExamId, bool withDetails = false);
    Task<BaseResponse> Delete(int  UserExamId);
    Task<ListResponse<UserExamWithResultDto>> GetUserExamsByExamId(int examId, CancellationToken cancellationToken);

    Task<SingleResponse<UserExamDetailsResponseDto>> GetUserExamDetails(int userExamId, CancellationToken cancellationToken);

    Task<ListResponse<UserExamReportDto>> GetUserExamReport(CancellationToken cancellationToken);

}



