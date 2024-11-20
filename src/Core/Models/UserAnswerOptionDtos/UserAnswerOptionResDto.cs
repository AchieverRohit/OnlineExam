
namespace thinkschool.OnlineExam.Core.Models
{

    public class UserAnswerOptionResDto:  IMapFrom<UserAnswerOption>
    {     
        
        public int UserAnswerId { get; set; }
        
        public int OptionId { get; set; }
    }

   public class UserAnswerOptionResDetailDto: UserAnswerOptionResDto, IMapFrom<UserAnswerOption>
   {
   
        public virtual UserAnswerResDto? UserAnswerOptionUserAnswerIdfk { get; set; }
        public virtual OptionResDto? UserAnswerOptionOptionIdfk { get; set; }
   }


}

