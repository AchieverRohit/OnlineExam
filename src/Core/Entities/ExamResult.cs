


namespace thinkschool.OnlineExam.Core.Entities
{
    public class ExamResult
    {
        
        public int ExamResultId { get; set; }
        
        public int UserExamId { get; set; }
        
        public decimal TotalObtainedMarks { get; set; }
        
        public string ResultStatus { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime UpdatedOn { get; set; }
        public virtual UserExam ExamResultUserExamIdfk { get; set; }
    }
}

