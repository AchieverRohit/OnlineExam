namespace thinkschool.OnlineExam.Core.Models.Base;
public class BaseResponse
{
    public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
    public List<ResponseMessage> Messages { get; set; } = [];
    public BaseResponse() { }
    public BaseResponse(HttpStatusCode code, params ResponseMessage[] messages)
    {
        Status = code;
        Messages = messages?.ToList() ?? [];
    }
}
public class SingleResponse<T> : BaseResponse where T : class
{
    public T Data { get; set; }
    public SingleResponse() : base() { }
    public SingleResponse(HttpStatusCode code, params ResponseMessage[] messages)
        : base(code, messages) { }
}
public class ListResponse<T> : BaseResponse where T : class
{
    public IEnumerable<T> Data { get; set; } = [];
    public PageDetails PageInfo { get; set; }
    public ListResponse() : base() { }
    public ListResponse(HttpStatusCode code, params ResponseMessage[] messages)
        : base(code, messages) { }
}
public class ResponseMessage
{
    public string? Message { get; set; }
    public string? Description { get; set; }
}

