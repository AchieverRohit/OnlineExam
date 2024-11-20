
namespace thinkschool.OnlineExam.Core.Models
{

    public class GetAllSectionResultReqDto : BasePagination
    {     
     
        public FilterExpression<int>? SectionResultId { get; set; } 
     
        public FilterExpression<int>? SectionId { get; set; } 
     
        public FilterExpression<int>? UserExamId { get; set; } 
     
        public FilterExpression<int>? QuestionsAttempted { get; set; } 
     
        public FilterExpression<decimal>? MarksObtained { get; set; } 
     
        public FilterExpression<string>? ResultStatus { get; set; } 
     }

    public class AddSectionResultReqDto : IMapTo<SectionResult>
    {
        
        public int SectionId { get; set; }
        
        public int UserExamId { get; set; }
        
        public int QuestionsAttempted { get; set; }
        
        public decimal MarksObtained { get; set; }
        
        public string ResultStatus { get; set; }
    }

    public class UpdateSectionResultReqDto :AddSectionResultReqDto, IMapTo<SectionResult>
    {
        public int SectionResultId { get; set; }
    }

}

