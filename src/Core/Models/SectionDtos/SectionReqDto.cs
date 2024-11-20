
namespace thinkschool.OnlineExam.Core.Models
{

    public class GetAllSectionReqDto : BasePagination
    {     
     
        public FilterExpression<int>? SectionId { get; set; } 
     
        public FilterExpression<int>? ExamId { get; set; } 
     
        public FilterExpression<string>? Title { get; set; } 
     
        public FilterExpression<int>? TotalQuestions { get; set; } 
     
        public FilterExpression<decimal>? TotalMarks { get; set; } 
     
        public FilterExpression<decimal>? PassingMarks { get; set; } 
     
        public FilterExpression<decimal>? WeightagePercentage { get; set; } 
     
        public FilterExpression<string>? CreatedBy { get; set; } 
     
        public FilterExpression<DateTime>? CreatedOn { get; set; } 
     
        public FilterExpression<DateTime>? UpdatedOn { get; set; } 
     }

    public class AddSectionReqDto : IMapTo<Section>
    {
        
        public int ExamId { get; set; }
        
        public string Title { get; set; }
        
        public int TotalQuestions { get; set; }
        
        public decimal TotalMarks { get; set; }
        
        public decimal PassingMarks { get; set; }
        
        public decimal WeightagePercentage { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime UpdatedOn { get; set; }
    }

    public class UpdateSectionReqDto :AddSectionReqDto, IMapTo<Section>
    {
        public int SectionId { get; set; }
    }

}

