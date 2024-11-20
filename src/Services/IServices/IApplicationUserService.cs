
namespace thinkschool.OnlineExam.Services.IServices;
public interface IApplicationUserService
{
 
    Task<ListResponse<ApplicationUserResDto>> GetAll(GetAllApplicationUserReqDto? requestDto);
    Task<SingleResponse<ApplicationUserResDto>> Save(AddApplicationUserReqDto requestDto);
    Task<SingleResponse<ApplicationUserResDto>> Update(UpdateApplicationUserReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(string  Id, bool withDetails = false);
    Task<BaseResponse> Delete(string  Id);
  
}



