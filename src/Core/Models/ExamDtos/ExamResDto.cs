
namespace thinkschool.OnlineExam.Core.Models
{

    public class ExamResDto:  IMapFrom<Exam>
    {     
        
        public int ExamId { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public double Duration { get; set; }
        
        public int TotalQuestions { get; set; }
        
        public decimal TotalMarks { get; set; }
        
        public decimal PassingMarks { get; set; }
        
        public bool IsRandomized { get; set; }
        
        public bool IsActive { get; set; }
        
        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime UpdatedOn { get; set; }
    }

   public class ExamResDetailDto: ExamResDto, IMapFrom<Exam>
   {
        public virtual List<SectionResDto>? SectionExams { get; set; }
     public virtual List<UserExamResDto>? UserExamExams { get; set; }

   }


}

