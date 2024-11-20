
namespace thinkschool.OnlineExam.Core.Models
{

    public class GetAllQuestionReqDto : BasePagination
    {     
     
        public FilterExpression<int>? QuestionId { get; set; } 
     
        public FilterExpression<int>? SectionId { get; set; } 
     
        public FilterExpression<string>? QuestionText { get; set; } 
     
        public FilterExpression<bool>? IsMedia { get; set; } 
     
        public FilterExpression<string>? MediaType { get; set; } 
     
        public FilterExpression<string>? MediaURL { get; set; } 
     
        public FilterExpression<bool>? IsMultipleChoice { get; set; } 
     
        public FilterExpression<bool>? IsFromQuestionBank { get; set; } 
     
        public FilterExpression<decimal>? QuestionMaxMarks { get; set; } 
     
        public FilterExpression<string>? CreatedBy { get; set; } 
     
        public FilterExpression<DateTime>? CreatedOn { get; set; } 
     
        public FilterExpression<DateTime>? UpdatedOn { get; set; } 
     }

    public class AddQuestionReqDto : IMapTo<Question>
    {
        
        public int SectionId { get; set; }
        
        public string QuestionText { get; set; }
        
        public bool IsMedia { get; set; }
        
        public string MediaType { get; set; }
        
        public string MediaURL { get; set; }
        
        public bool IsMultipleChoice { get; set; }
        
        public bool IsFromQuestionBank { get; set; }
        
        public decimal QuestionMaxMarks { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime UpdatedOn { get; set; }
    }

    public class UpdateQuestionReqDto :AddQuestionReqDto, IMapTo<Question>
    {
        public int QuestionId { get; set; }
    }

}

