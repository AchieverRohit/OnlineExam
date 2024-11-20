
namespace thinkschool.OnlineExam.Core.Models
{

    public class SectionResDto:  IMapFrom<Section>
    {     
        
        public int SectionId { get; set; }
        
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

   public class SectionResDetailDto: SectionResDto, IMapFrom<Section>
   {
        public virtual List<QuestionResDto>? QuestionSections { get; set; }
     public virtual List<SectionResultResDto>? SectionResultSections { get; set; }
     public virtual List<UserAnswerResDto>? UserAnswerSections { get; set; }

        public virtual ExamResDto? SectionExamIdfk { get; set; }
   }


}

