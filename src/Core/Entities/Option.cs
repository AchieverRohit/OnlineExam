


namespace thinkschool.OnlineExam.Core.Entities
{
    public class Option
    {
        
        public int OptionId { get; set; }
        
        public int QuestionId { get; set; }
        
        public string OptionText { get; set; }
        
        public bool IsCorrect { get; set; }
        
        public decimal Marks { get; set; }
        public virtual List<UserAnswerOption> UserAnswerOptionOptions { get; set; }
        public virtual Question OptionQuestionIdfk { get; set; }
    }
}

