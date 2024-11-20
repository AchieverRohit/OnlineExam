namespace thinkschool.OnlineExam.Services.IServices;
public interface IServiceFactory
{
    IServiceScope CreateScope();
    T GetService<T>(IServiceScope scope);
}
