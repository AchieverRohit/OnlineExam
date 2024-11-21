
namespace thinkschool.OnlineExam.Core.Models
{

    public class UserExamResDto:  IMapFrom<UserExam>
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
    }

   public class UserExamResDetailDto: UserExamResDto, IMapFrom<UserExam>
   {
        public virtual List<ExamResultResDto>? ExamResultUserExams { get; set; }
     public virtual List<SectionResultResDto>? SectionResultUserExams { get; set; }
     public virtual List<UserAnswerResDto>? UserAnswerUserExams { get; set; }

        public virtual ApplicationUserResDto? UserExamUserIdfk { get; set; }
        public virtual ExamResDto? UserExamExamIdfk { get; set; }
   }

    public class UserExamWithResultDto
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
        public ExamResultDto ExamResult { get; set; }
    }

    public class ExamResultDto
    {
        public int ExamResultId { get; set; }
        public decimal TotalObtainedMarks { get; set; }
        public string ResultStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}

