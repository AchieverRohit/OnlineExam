
namespace thinkschool.OnlineExam.Core.Models
{

    public class SectionResultResDto:  IMapFrom<SectionResult>
    {     
        
        public int SectionResultId { get; set; }
        
        public int SectionId { get; set; }
        
        public int UserExamId { get; set; }
        
        public int QuestionsAttempted { get; set; }
        
        public decimal MarksObtained { get; set; }
        
        public string ResultStatus { get; set; }
    }

   public class SectionResultResDetailDto: SectionResultResDto, IMapFrom<SectionResult>
   {
   
        public virtual SectionResDto? SectionResultSectionIdfk { get; set; }
        public virtual UserExamResDto? SectionResultUserExamIdfk { get; set; }
   }


}

