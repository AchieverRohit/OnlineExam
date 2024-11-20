namespace thinkschool.OnlineExam.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services)
    {
        // Add services to the container.
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen().AddSwaggerGenNewtonsoftSupport();
        // add versioning support and setup the swagger ui to support multiple versions via dropdown
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;

            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(configure =>
        {
            configure.SubstituteApiVersionInUrl = true;
            configure.GroupNameFormat = "'v'VVV";
            configure.DefaultApiVersion = new ApiVersion(1, 0);
        });

        // automapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
