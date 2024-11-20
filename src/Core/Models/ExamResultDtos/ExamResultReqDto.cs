
namespace thinkschool.OnlineExam.Core.Models
{

    public class GetAllExamResultReqDto : BasePagination
    {     
     
        public FilterExpression<int>? ExamResultId { get; set; } 
     
        public FilterExpression<int>? UserExamId { get; set; } 
     
        public FilterExpression<decimal>? TotalObtainedMarks { get; set; } 
     
        public FilterExpression<string>? ResultStatus { get; set; } 
     
        public FilterExpression<string>? CreatedBy { get; set; } 
     
        public FilterExpression<DateTime>? CreatedOn { get; set; } 
     
        public FilterExpression<DateTime>? UpdatedOn { get; set; } 
     }

    public class AddExamResultReqDto : IMapTo<ExamResult>
    {
        
        public int UserExamId { get; set; }
        
        public decimal TotalObtainedMarks { get; set; }
        
        public string ResultStatus { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime UpdatedOn { get; set; }
    }

    public class UpdateExamResultReqDto :AddExamResultReqDto, IMapTo<ExamResult>
    {
        public int ExamResultId { get; set; }
    }

}

