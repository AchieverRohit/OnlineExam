
namespace thinkschool.OnlineExam.Services.IServices;
public interface IOptionService
{
 
    Task<ListResponse<OptionResDto>> GetAll(GetAllOptionReqDto? requestDto);
    Task<SingleResponse<OptionResDto>> Save(AddOptionReqDto requestDto);
    Task<SingleResponse<OptionResDto>> Update(UpdateOptionReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  OptionId, bool withDetails = false);
    Task<BaseResponse> Delete(int  OptionId);
  
}



