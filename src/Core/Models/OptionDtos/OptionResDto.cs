
namespace thinkschool.OnlineExam.Core.Models
{

    public class OptionResDto:  IMapFrom<Option>
    {     
        
        public int OptionId { get; set; }
        
        public int QuestionId { get; set; }
        
        public string OptionText { get; set; }
        
        public bool IsCorrect { get; set; }
        
        public decimal Marks { get; set; }
    }

   public class OptionResDetailDto: OptionResDto, IMapFrom<Option>
   {
        public virtual List<UserAnswerOptionResDto>? UserAnswerOptionOptions { get; set; }

        public virtual QuestionResDto? OptionQuestionIdfk { get; set; }
   }


}

