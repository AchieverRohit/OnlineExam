namespace thinkschool.OnlineExam.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the service factory
        services.AddSingleton<IServiceFactory, ServiceFactory>();
        // ServicesCollection Registration 
        services.AddScoped<IServicesCollection, ServicesCollection>();
        // data access Layer
        services.AddDataAccessLayer(configuration);
        services.AddScoped<IApplicationUserService, ApplicationUserService>();
        services.AddScoped<IExamResultService, ExamResultService>();
        services.AddScoped<IExamService, ExamService>();
        services.AddScoped<IOptionService, OptionService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<ISectionResultService, SectionResultService>();
        services.AddScoped<ISectionService, SectionService>();
        services.AddScoped<IUserAnswerService, UserAnswerService>();
        services.AddScoped<IUserExamService, UserExamService>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}