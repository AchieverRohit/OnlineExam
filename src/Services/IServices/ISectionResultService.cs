
namespace thinkschool.OnlineExam.Services.IServices;
public interface ISectionResultService
{
 
    Task<ListResponse<SectionResultResDto>> GetAll(GetAllSectionResultReqDto? requestDto);
    Task<SingleResponse<SectionResultResDto>> Save(AddSectionResultReqDto requestDto);
    Task<SingleResponse<SectionResultResDto>> Update(UpdateSectionResultReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  SectionResultId, bool withDetails = false);
    Task<BaseResponse> Delete(int  SectionResultId);
  
}



