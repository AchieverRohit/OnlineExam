
namespace thinkschool.OnlineExam.DataAccess.Persistence;
public static class DbConfiguration
{
    public static void ApplyEntityConfigurations(this ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>(entity =>
        {
entity.HasKey(e => new { e.Id });
        entity.Property(x => x.Id);
        entity.Property(x => x.Id).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.FirstName).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.LastName).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.UserName).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.NormalizedUserName).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.Email).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.NormalizedEmail).HasColumnType("nvarchar").HasMaxLength(256);
        entity.Property(x => x.EmailConfirmed).HasColumnType("bit").IsRequired();
        entity.Property(x => x.PasswordHash).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.SecurityStamp).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.ConcurrencyStamp).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(255);
        entity.Property(x => x.PhoneNumberConfirmed).HasColumnType("bit").IsRequired();
        entity.Property(x => x.TwoFactorEnabled).HasColumnType("bit").IsRequired();
        entity.Property(x => x.LockoutEnd).HasColumnType("datetimeoffset");
        entity.Property(x => x.LockoutEnabled).HasColumnType("bit").IsRequired();
        entity.Property(x => x.AccessFailedCount).HasColumnType("int").IsRequired();
        entity.HasIndex(x => new { x.NormalizedEmail }).IsUnique(false).HasDatabaseName("EmailIndex");
        entity.HasIndex(x => new { x.NormalizedUserName }).IsUnique(true).HasDatabaseName("UserNameIndex");

});

builder.Entity<ExamResult>(entity =>
{
entity.HasKey(e => new { e.ExamResultId });
        entity.Property(x => x.ExamResultId).ValueGeneratedOnAdd();
        entity.Property(x => x.ExamResultId).HasColumnType("int").IsRequired();
        entity.Property(x => x.UserExamId).HasColumnType("int").IsRequired();
        entity.Property(x => x.TotalObtainedMarks).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.ResultStatus).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.CreatedOn).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.UpdatedOn).HasColumnType("datetime2").IsRequired();
        entity.HasIndex(x => new { x.UserExamId }).IsUnique(false).HasDatabaseName("IX_ExamResults_UserExamId");
        entity.HasOne(x => x.ExamResultUserExamIdfk).WithMany(x => x.ExamResultUserExams).HasForeignKey(x => x.UserExamId).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<Exam>(entity =>
{
entity.HasKey(e => new { e.ExamId });
        entity.Property(x => x.ExamId).ValueGeneratedOnAdd();
        entity.Property(x => x.ExamId).HasColumnType("int").IsRequired();
        entity.Property(x => x.Title).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.StartDate).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.EndDate).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.Duration).HasColumnType("float").IsRequired();
        entity.Property(x => x.TotalQuestions).HasColumnType("int").IsRequired();
        entity.Property(x => x.TotalMarks).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.PassingMarks).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.IsRandomized).HasColumnType("bit").IsRequired();
        entity.Property(x => x.IsActive).HasColumnType("bit").IsRequired();
        entity.Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.CreatedOn).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.UpdatedOn).HasColumnType("datetime2").IsRequired();

});
builder.Entity<Option>(entity =>
{
entity.HasKey(e => new { e.OptionId });
        entity.Property(x => x.OptionId).ValueGeneratedOnAdd();
        entity.Property(x => x.OptionId).HasColumnType("int").IsRequired();
        entity.Property(x => x.QuestionId).HasColumnType("int").IsRequired();
        entity.Property(x => x.OptionText).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.IsCorrect).HasColumnType("bit").IsRequired();
        entity.Property(x => x.Marks).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.HasIndex(x => new { x.QuestionId }).IsUnique(false).HasDatabaseName("IX_Options_QuestionId");
        entity.HasOne(x => x.OptionQuestionIdfk).WithMany(x => x.OptionQuestions).HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<Question>(entity =>
{
entity.HasKey(e => new { e.QuestionId });
        entity.Property(x => x.QuestionId).ValueGeneratedOnAdd();
        entity.Property(x => x.QuestionId).HasColumnType("int").IsRequired();
        entity.Property(x => x.SectionId).HasColumnType("int").IsRequired();
        entity.Property(x => x.QuestionText).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.IsMedia).HasColumnType("bit").IsRequired();
        entity.Property(x => x.MediaType).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.MediaURL).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.IsMultipleChoice).HasColumnType("bit").IsRequired();
        entity.Property(x => x.IsFromQuestionBank).HasColumnType("bit").IsRequired();
        entity.Property(x => x.QuestionMaxMarks).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.CreatedOn).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.UpdatedOn).HasColumnType("datetime2").IsRequired();
        entity.HasIndex(x => new { x.SectionId }).IsUnique(false).HasDatabaseName("IX_Questions_SectionId");
        entity.HasOne(x => x.QuestionSectionIdfk).WithMany(x => x.QuestionSections).HasForeignKey(x => x.SectionId).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<SectionResult>(entity =>
{
entity.HasKey(e => new { e.SectionResultId });
        entity.Property(x => x.SectionResultId).ValueGeneratedOnAdd();
        entity.Property(x => x.SectionResultId).HasColumnType("int").IsRequired();
        entity.Property(x => x.SectionId).HasColumnType("int").IsRequired();
        entity.Property(x => x.UserExamId).HasColumnType("int").IsRequired();
        entity.Property(x => x.QuestionsAttempted).HasColumnType("int").IsRequired();
        entity.Property(x => x.MarksObtained).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.ResultStatus).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.HasIndex(x => new { x.SectionId }).IsUnique(false).HasDatabaseName("IX_SectionResults_SectionId");
        entity.HasIndex(x => new { x.UserExamId }).IsUnique(false).HasDatabaseName("IX_SectionResults_UserExamId");
        entity.HasOne(x => x.SectionResultSectionIdfk).WithMany(x => x.SectionResultSections).HasForeignKey(x => x.SectionId).OnDelete(DeleteBehavior.NoAction);
        entity.HasOne(x => x.SectionResultUserExamIdfk).WithMany(x => x.SectionResultUserExams).HasForeignKey(x => x.UserExamId).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<Section>(entity =>
{
entity.HasKey(e => new { e.SectionId });
        entity.Property(x => x.SectionId).ValueGeneratedOnAdd();
        entity.Property(x => x.SectionId).HasColumnType("int").IsRequired();
        entity.Property(x => x.ExamId).HasColumnType("int").IsRequired();
        entity.Property(x => x.Title).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.TotalQuestions).HasColumnType("int").IsRequired();
        entity.Property(x => x.TotalMarks).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.PassingMarks).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.WeightagePercentage).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.CreatedOn).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.UpdatedOn).HasColumnType("datetime2").IsRequired();
        entity.HasIndex(x => new { x.ExamId }).IsUnique(false).HasDatabaseName("IX_Sections_ExamId");
        entity.HasOne(x => x.SectionExamIdfk).WithMany(x => x.SectionExams).HasForeignKey(x => x.ExamId).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<UserAnswerOption>(entity =>
{
entity.HasKey(e => new { e.UserAnswerId, e.OptionId });
        entity.Property(x => x.OptionId);
        entity.Property(x => x.UserAnswerId);
        entity.Property(x => x.UserAnswerId).HasColumnType("int").IsRequired();
        entity.Property(x => x.OptionId).HasColumnType("int").IsRequired();
        entity.HasOne(x => x.UserAnswerOptionUserAnswerIdfk).WithMany(x => x.UserAnswerOptionUserAnswers).HasForeignKey(x => x.UserAnswerId).OnDelete(DeleteBehavior.NoAction);
        entity.HasOne(x => x.UserAnswerOptionOptionIdfk).WithMany(x => x.UserAnswerOptionOptions).HasForeignKey(x => x.OptionId).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<UserAnswer>(entity =>
{
entity.HasKey(e => new { e.UserAnswerId });
        entity.Property(x => x.UserAnswerId).ValueGeneratedOnAdd();
        entity.Property(x => x.UserAnswerId).HasColumnType("int").IsRequired();
        entity.Property(x => x.QuestionId).HasColumnType("int").IsRequired();
        entity.Property(x => x.UserExamId).HasColumnType("int").IsRequired();
        entity.Property(x => x.SectionId).HasColumnType("int").IsRequired();
        entity.HasIndex(x => new { x.QuestionId }).IsUnique(false).HasDatabaseName("IX_UserAnswers_QuestionId");
        entity.HasIndex(x => new { x.SectionId }).IsUnique(false).HasDatabaseName("IX_UserAnswers_SectionId");
        entity.HasIndex(x => new { x.UserExamId }).IsUnique(false).HasDatabaseName("IX_UserAnswers_UserExamId");
        entity.HasOne(x => x.UserAnswerQuestionIdfk).WithMany(x => x.UserAnswerQuestions).HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.NoAction);
        entity.HasOne(x => x.UserAnswerUserExamIdfk).WithMany(x => x.UserAnswerUserExams).HasForeignKey(x => x.UserExamId).OnDelete(DeleteBehavior.NoAction);
        entity.HasOne(x => x.UserAnswerSectionIdfk).WithMany(x => x.UserAnswerSections).HasForeignKey(x => x.SectionId).OnDelete(DeleteBehavior.NoAction);

});
builder.Entity<UserExam>(entity =>
{
entity.HasKey(e => new { e.UserExamId });
        entity.Property(x => x.UserExamId).ValueGeneratedOnAdd();
        entity.Property(x => x.UserExamId).HasColumnType("int").IsRequired();
        entity.Property(x => x.UserId).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();
        entity.Property(x => x.ExamId).HasColumnType("int").IsRequired();
        entity.Property(x => x.StartedOn).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.FinishedOn).HasColumnType("datetime2");
        entity.Property(x => x.ExamStatus).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.TotalMarks).HasColumnType("decimal").HasPrecision(18, 2);
        entity.Property(x => x.IsAutoSubmitted).HasColumnType("bit");
        entity.Property(x => x.NoOfAttempt).HasColumnType("int").IsRequired();
        entity.Property(x => x.CreatedBy).HasColumnType("nvarchar").HasMaxLength(255).IsRequired();
        entity.Property(x => x.CreatedOn).HasColumnType("datetime2").IsRequired();
        entity.Property(x => x.UpdatedOn).HasColumnType("datetime2").IsRequired();
        entity.HasIndex(x => new { x.ExamId }).IsUnique(false).HasDatabaseName("IX_UserExams_ExamId");
        entity.HasIndex(x => new { x.UserId }).IsUnique(false).HasDatabaseName("IX_UserExams_UserId");
        entity.HasOne(x => x.UserExamUserIdfk).WithMany(x => x.UserExamUsers).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        entity.HasOne(x => x.UserExamExamIdfk).WithMany(x => x.UserExamExams).HasForeignKey(x => x.ExamId).OnDelete(DeleteBehavior.NoAction);

});
        // add entity configurations here 
    }
}
