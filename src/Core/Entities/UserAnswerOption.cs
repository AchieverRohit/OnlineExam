


namespace thinkschool.OnlineExam.Core.Entities
{
    public class UserAnswerOption
    {
        
        public int UserAnswerId { get; set; }
        
        public int OptionId { get; set; }
        public virtual UserAnswer UserAnswerOptionUserAnswerIdfk { get; set; }
        public virtual Option UserAnswerOptionOptionIdfk { get; set; }
    }
}

