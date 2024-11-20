
namespace thinkschool.OnlineExam.Core.Models
{

    public class QuestionResDto:  IMapFrom<Question>
    {     
        
        public int QuestionId { get; set; }
        
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

   public class QuestionResDetailDto: QuestionResDto, IMapFrom<Question>
   {
        public virtual List<OptionResDto>? OptionQuestions { get; set; }
     public virtual List<UserAnswerResDto>? UserAnswerQuestions { get; set; }

        public virtual SectionResDto? QuestionSectionIdfk { get; set; }
   }


}

