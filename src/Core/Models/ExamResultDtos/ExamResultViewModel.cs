using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinkschool.OnlineExam.Core.Models.ExamResultDtos
{
    public class ExamResultViewModel
    {
        public int UserExamId { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
        public string ResultStatus { get; set; }
    }
}
