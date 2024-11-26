
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace thinkschool.OnlineExam.DataAccess.Persistence;
public class AppDbContext : IdentityDbContext<ApplicationUser> , IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder = optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyEntityConfigurations();
        base.OnModelCreating(modelBuilder);
    }

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
