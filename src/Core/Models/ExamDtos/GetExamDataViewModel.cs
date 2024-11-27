using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinkschool.OnlineExam.Core.Models.ExamDtos
{
    public class GetExamDataViewModel
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string ExamStatus { get; set; }
        public int TotalStudents { get; set; }
        public int PassedStudents { get; set; }
        public decimal OverallPassStudentsPercentage { get; set; }
    }
}
