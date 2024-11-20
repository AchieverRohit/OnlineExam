
namespace thinkschool.OnlineExam.Core.Models
{

    public class GetAllExamReqDto : BasePagination
    {     
     
        public FilterExpression<int>? ExamId { get; set; } 
     
        public FilterExpression<string>? Title { get; set; } 
     
        public FilterExpression<string>? Description { get; set; } 
     
        public FilterExpression<DateTime>? StartDate { get; set; } 
     
        public FilterExpression<DateTime>? EndDate { get; set; } 
     
        public FilterExpression<double>? Duration { get; set; } 
     
        public FilterExpression<int>? TotalQuestions { get; set; } 
     
        public FilterExpression<decimal>? TotalMarks { get; set; } 
     
        public FilterExpression<decimal>? PassingMarks { get; set; } 
     
        public FilterExpression<bool>? IsRandomized { get; set; } 
     
        public FilterExpression<bool>? IsActive { get; set; } 
     
        public FilterExpression<string>? CreatedBy { get; set; } 
     
        public FilterExpression<DateTime>? CreatedOn { get; set; } 
     
        public FilterExpression<DateTime>? UpdatedOn { get; set; } 
     }

    public class AddExamReqDto : IMapTo<Exam>
    {
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public double Duration { get; set; }
        
        public int TotalQuestions { get; set; }
        
        public decimal TotalMarks { get; set; }
        
        public decimal PassingMarks { get; set; }
        
        public bool IsRandomized { get; set; }
        
        public bool IsActive { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime UpdatedOn { get; set; }
    }

    public class UpdateExamReqDto :AddExamReqDto, IMapTo<Exam>
    {
        public int ExamId { get; set; }
    }

}

