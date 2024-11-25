using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinkschool.OnlineExam.Core.Models.UserAnswerDtos
{
    public class SubmitAnswerDto
    {
        public int QuestionId { get; set; }
        public int UserExamId { get; set; }
        public int SectionId { get; set; }
        public List<int> OptionIds { get; set; }
    }
}
