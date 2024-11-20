


namespace thinkschool.OnlineExam.Core.Entities
{
    public class UserAnswer
    {
        
        public int UserAnswerId { get; set; }
        
        public int QuestionId { get; set; }
        
        public int UserExamId { get; set; }
        
        public int SectionId { get; set; }
        public virtual List<UserAnswerOption> UserAnswerOptionUserAnswers { get; set; }
        public virtual Question UserAnswerQuestionIdfk { get; set; }
        public virtual UserExam UserAnswerUserExamIdfk { get; set; }
        public virtual Section UserAnswerSectionIdfk { get; set; }
    }
}

