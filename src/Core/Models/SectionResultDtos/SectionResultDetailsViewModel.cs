using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinkschool.OnlineExam.Core.Models.SectionResultDtos
{
    public class SectionResultDetailsViewModel
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int QuestionsAttempted { get; set; }
        public decimal SectionTotalMarks { get; set; }
        public decimal ObtainedMarks { get; set; }
        public string SectionResultStatus { get; set; }
    }
}
