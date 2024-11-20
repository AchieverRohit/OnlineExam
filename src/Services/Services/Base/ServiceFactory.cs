namespace thinkschool.OnlineExam.Services.Services;
public class ServiceFactory(IServiceProvider serviceProvider) : IServiceFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    public IServiceScope CreateScope()
    {
        return _serviceProvider.CreateScope();
    }
    public T GetService<T>(IServiceScope scope)
    {
        return scope.ServiceProvider.GetRequiredService<T>();
    }
}
