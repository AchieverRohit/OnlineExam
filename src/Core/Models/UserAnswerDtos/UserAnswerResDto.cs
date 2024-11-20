
namespace thinkschool.OnlineExam.Core.Models
{

    public class UserAnswerResDto:  IMapFrom<UserAnswer>
    {     
        
        public int UserAnswerId { get; set; }
        
        public int QuestionId { get; set; }
        
        public int UserExamId { get; set; }
        
        public int SectionId { get; set; }
    }

   public class UserAnswerResDetailDto: UserAnswerResDto, IMapFrom<UserAnswer>
   {
        public virtual List<UserAnswerOptionResDto>? UserAnswerOptionUserAnswers { get; set; }

        public virtual QuestionResDto? UserAnswerQuestionIdfk { get; set; }
        public virtual UserExamResDto? UserAnswerUserExamIdfk { get; set; }
        public virtual SectionResDto? UserAnswerSectionIdfk { get; set; }
   }


}

