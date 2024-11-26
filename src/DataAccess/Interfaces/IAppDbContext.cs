
namespace thinkschool.OnlineExam.DataAccess.Interfaces;
public interface IAppDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ExamResult> ExamResults { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<SectionResult> SectionResults { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<UserAnswerOption> UserAnswerOptions { get; set; }
    public DbSet<UserAnswer> UserAnswers { get; set; }
    public DbSet<UserExam> UserExams { get; set; }
    public DbSet<Lookup> Lookups { get; set; }
}
