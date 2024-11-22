
namespace thinkschool.OnlineExam.Services.IServices;
public interface ISectionResultService
{
 
    Task<ListResponse<SectionResultResDto>> GetAll(GetAllSectionResultReqDto? requestDto);
    Task<SingleResponse<SectionResultResDto>> Save(AddSectionResultReqDto requestDto);
    Task<SingleResponse<SectionResultResDto>> Update(UpdateSectionResultReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(int  SectionResultId, bool withDetails = false);
    Task<BaseResponse> Delete(int  SectionResultId);

    /// <summary>
    /// Generates a result for a section of an exam.
    /// </summary>
    /// <param name="sectionId">The ID of the section.</param>
    /// <param name="userExamId">The ID of the user's exam attempt.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A response with the generated section result.</returns>
    Task<SingleResponse<SectionResultResDto>> GenerateSectionResult(int sectionId, int userExamId, CancellationToken cancellationToken);
}



