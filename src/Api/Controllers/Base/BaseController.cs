namespace thinkschool.OnlineExam.Api.Controllers.Base;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    // base BaseController
    protected ActionResult HandleResult(BaseResponse result, bool isFlat = false)
    {

        return result.Status switch
        {
            HttpStatusCode.OK => isFlat ? Ok((result as dynamic).Data) : Ok(result),
            HttpStatusCode.NotFound => NotFound(result),
            HttpStatusCode.BadRequest => BadRequest(result),
            _ => StatusCode((int)result.Status, result)
        };
    }
}
