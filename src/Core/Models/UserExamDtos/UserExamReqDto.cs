
namespace thinkschool.OnlineExam.Core.Models
{

    public class GetAllUserExamReqDto : BasePagination
    {     
     
        public FilterExpression<int>? UserExamId { get; set; } 
     
        public FilterExpression<string>? UserId { get; set; } 
     
        public FilterExpression<int>? ExamId { get; set; } 
     
        public FilterExpression<DateTime>? StartedOn { get; set; } 
     
        public FilterExpression<DateTime?>? FinishedOn { get; set; } 
     
        public FilterExpression<string>? ExamStatus { get; set; } 
     
        public FilterExpression<decimal?>? TotalMarks { get; set; } 
     
        public FilterExpression<bool?>? IsAutoSubmitted { get; set; } 
     
        public FilterExpression<int>? NoOfAttempt { get; set; } 
     
        public FilterExpression<string>? CreatedBy { get; set; } 
     
        public FilterExpression<DateTime>? CreatedOn { get; set; } 
     
        public FilterExpression<DateTime>? UpdatedOn { get; set; } 
     }

    public class AddUserExamReqDto : IMapTo<UserExam>
    {
        
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

    public class UpdateUserExamReqDto :AddUserExamReqDto, IMapTo<UserExam>
    {
        public int UserExamId { get; set; }
    }

}

