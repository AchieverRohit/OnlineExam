namespace thinkschool.OnlineExam.Core;
public static class DependencyInjection
{
    public static IServiceCollection AddCoreLayer(this IServiceCollection services)
    {
        // automapper
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new MappingProfile(Assembly.GetExecutingAssembly()));
        });
        // FluentValidation registration
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation(); // This line ensures that FluentValidation is used for model validation
        return services;
    }
}
