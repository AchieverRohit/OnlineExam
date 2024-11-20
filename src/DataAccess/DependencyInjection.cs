namespace thinkschool.OnlineExam.DataAccess;
public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        // add core layer
        services.AddCoreLayer();
        // configure DbContext
        services.AddDbContext<AppDbContext>(options => {
            options.UseSqlServer(configuration[ConstantsValues.ConnectionString]);
        });
        // Register the IAppDbContext to use AppDbContext.
        services.AddScoped<IAppDbContext, AppDbContext>();
        // Repositories Registration 
        services.AddScoped<IBaseRepository, BaseRepository>();
        return services;
    }
   
}
