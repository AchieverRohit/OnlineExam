


namespace thinkschool.OnlineExam.Core.Entities
{
    public class Section
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
        public virtual List<Question> QuestionSections { get; set; }
        public virtual List<SectionResult> SectionResultSections { get; set; }
        public virtual List<UserAnswer> UserAnswerSections { get; set; }
        public virtual Exam SectionExamIdfk { get; set; }
    }
}

