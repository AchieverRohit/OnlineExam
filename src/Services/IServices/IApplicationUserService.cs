
namespace thinkschool.OnlineExam.Services.IServices;
public interface IApplicationUserService
{
 
    Task<ListResponse<ApplicationUserResDto>> GetAll(GetAllApplicationUserReqDto? requestDto);
    Task<SingleResponse<ApplicationUserResDto>> Save(AddApplicationUserReqDto requestDto);
    Task<SingleResponse<ApplicationUserResDto>> Update(UpdateApplicationUserReqDto requestDto);
    Task<SingleResponse<dynamic>> GetById(string  Id, bool withDetails = false);
    Task<BaseResponse> Delete(string  Id);

    /// <summary>
    /// Retrieves a list of types based on the provided type parameter.
    /// </summary>
    /// <param name="type">The type of data to retrieve (e.g., "role", "questionType").</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A list response containing the requested type data.</returns>
    Task<ListResponse<string>> GetTypes(string type, CancellationToken cancellationToken);
}



