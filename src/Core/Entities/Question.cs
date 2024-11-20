


namespace thinkschool.OnlineExam.Core.Entities
{
    public class Question
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
        public virtual List<Option> OptionQuestions { get; set; }
        public virtual List<UserAnswer> UserAnswerQuestions { get; set; }
        public virtual Section QuestionSectionIdfk { get; set; }
    }
}

