


namespace thinkschool.OnlineExam.Core.Entities
{
    public class SectionResult
    {
        
        public int SectionResultId { get; set; }
        
        public int SectionId { get; set; }
        
        public int UserExamId { get; set; }
        
        public int QuestionsAttempted { get; set; }
        
        public decimal MarksObtained { get; set; }
        
        public string ResultStatus { get; set; }
        public virtual Section SectionResultSectionIdfk { get; set; }
        public virtual UserExam SectionResultUserExamIdfk { get; set; }
    }
}

