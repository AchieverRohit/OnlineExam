namespace thinkschool.OnlineExam.Core.Constants;
public static class ConstantsValues
{
    // add constants values here
    public  const string ConnectionString = "ConnectionStrings:SQLServerConnString";
    public  const string DatabaseMigratedSuccessfully = "Database migrated successfully";
    public  const string NotSupportDatabase = "We exclusively support SQL databases";
    public  const string MigrationError = "An error occurred while migrating the database.";
    public  const string UnhandledException = "An unhandled exception occurred during the request.";
    public  const string ApplicationJson = "application/json";
    public  const string RequestTimeLog = "Request {Method} {Path} took {ElapsedMilliseconds} ms";
    public const string NoRecord = "No record found linked to the given Id: {0}";
}
