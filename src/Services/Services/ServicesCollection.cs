namespace thinkschool.OnlineExam.Services.Services;
public class ServicesCollection : IServicesCollection, IDisposable
{
    private readonly IServiceScope _scope;
    public ServicesCollection(IServiceFactory serviceFactory)
    {
        _scope = serviceFactory.CreateScope();
        _lazyIApplicationUserService = new Lazy<IApplicationUserService>(_scope.ServiceProvider.GetRequiredService<IApplicationUserService>);
        _lazyIExamResultService = new Lazy<IExamResultService>(_scope.ServiceProvider.GetRequiredService<IExamResultService>);
        _lazyIExamService = new Lazy<IExamService>(_scope.ServiceProvider.GetRequiredService<IExamService>);
        _lazyIOptionService = new Lazy<IOptionService>(_scope.ServiceProvider.GetRequiredService<IOptionService>);
        _lazyIQuestionService = new Lazy<IQuestionService>(_scope.ServiceProvider.GetRequiredService<IQuestionService>);
        _lazyISectionResultService = new Lazy<ISectionResultService>(_scope.ServiceProvider.GetRequiredService<ISectionResultService>);
        _lazyISectionService = new Lazy<ISectionService>(_scope.ServiceProvider.GetRequiredService<ISectionService>);
        _lazyIUserAnswerService = new Lazy<IUserAnswerService>(_scope.ServiceProvider.GetRequiredService<IUserAnswerService>);
        _lazyIUserExamService = new Lazy<IUserExamService>(_scope.ServiceProvider.GetRequiredService<IUserExamService>);
        _lazyITokenService=new Lazy<ITokenService>(_scope.ServiceProvider.GetRequiredService<ITokenService>);
    }

    private readonly Lazy<IApplicationUserService> _lazyIApplicationUserService;
    private readonly Lazy<IExamResultService> _lazyIExamResultService;
    private readonly Lazy<IExamService> _lazyIExamService;
    private readonly Lazy<IOptionService> _lazyIOptionService;
    private readonly Lazy<IQuestionService> _lazyIQuestionService;
    private readonly Lazy<ISectionResultService> _lazyISectionResultService;
    private readonly Lazy<ISectionService> _lazyISectionService;
    private readonly Lazy<IUserAnswerService> _lazyIUserAnswerService;
    private readonly Lazy<IUserExamService> _lazyIUserExamService;
    private readonly Lazy<ITokenService> _lazyITokenService;
    public void Dispose()
    {
        _scope?.Dispose();
    }
    public IApplicationUserService ApplicationUserServices => _lazyIApplicationUserService.Value;
    public IExamResultService ExamResultServices => _lazyIExamResultService.Value;
    public IExamService ExamServices => _lazyIExamService.Value;
    public IOptionService OptionServices => _lazyIOptionService.Value;
    public IQuestionService QuestionServices => _lazyIQuestionService.Value;
    public ISectionResultService SectionResultServices => _lazyISectionResultService.Value;
    public ISectionService SectionServices => _lazyISectionService.Value;
    public IUserAnswerService UserAnswerServices => _lazyIUserAnswerService.Value;
    public IUserExamService UserExamServices => _lazyIUserExamService.Value;

    public ITokenService TokenService=> _lazyITokenService.Value;
}
