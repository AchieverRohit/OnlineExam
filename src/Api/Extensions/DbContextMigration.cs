namespace thinkschool.OnlineExam.Api.Extensions;
public static class DbContextMigration
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
        try
        {
            if (context.Database.IsSqlServer())
            {
                context.Database.SetCommandTimeout(120); // 2 Minutes
                context.Database.Migrate();
                logger.LogInformation(ConstantsValues.DatabaseMigratedSuccessfully);
            }
            else
            {
                logger.LogInformation(ConstantsValues.NotSupportDatabase);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ConstantsValues.MigrationError);
            throw;
        }
    }
}
