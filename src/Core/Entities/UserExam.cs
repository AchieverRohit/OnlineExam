


namespace thinkschool.OnlineExam.Core.Entities
{
    public class UserExam
    {
        
        public int UserExamId { get; set; }
        
        public string UserId { get; set; }
        
        public int ExamId { get; set; }
        
        public DateTime StartedOn { get; set; }
        
        public DateTime? FinishedOn { get; set; }
        
        public string ExamStatus { get; set; }
        
        public decimal? TotalMarks { get; set; }
        
        public bool? IsAutoSubmitted { get; set; }
        
        public int NoOfAttempt { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime UpdatedOn { get; set; }
        public virtual List<ExamResult> ExamResultUserExams { get; set; }
        public virtual List<SectionResult> SectionResultUserExams { get; set; }
        public virtual List<UserAnswer> UserAnswerUserExams { get; set; }
        public virtual ApplicationUser UserExamUserIdfk { get; set; }
        public virtual Exam UserExamExamIdfk { get; set; }
    }
}

