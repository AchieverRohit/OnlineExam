
namespace thinkschool.OnlineExam.Services.IServices;
public interface ISectionService
{
 
    Task<ListResponse<SectionResDto>> GetAll(GetAllSectionReqDto? requestDto);
    Task<SingleResponse<SectionResDto>> Save(AddSectionReqDto requestDto);
    Task<SingleResponse<SectionResDto>> Update(UpdateSectionReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  SectionId, bool withDetails = false);
    Task<BaseResponse> Delete(int  SectionId);
  
}



