using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using thinkschool.OnlineExam.Core.Models.SectionResultDtos;

namespace thinkschool.OnlineExam.Core.Models.ExamResultDtos
{
    public class ExamResultDetailsViewModel
    {
        public int ExamResultId { get; set; }
        public decimal TotalScore { get; set; }
        public string ResultStatus { get; set; }
        public List<SectionResultDetailsViewModel> SectionResults { get; set; }
    }
}
