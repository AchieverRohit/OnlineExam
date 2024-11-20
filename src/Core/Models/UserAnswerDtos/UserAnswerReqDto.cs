
namespace thinkschool.OnlineExam.Core.Models
{

    public class GetAllUserAnswerReqDto : BasePagination
    {     
     
        public FilterExpression<int>? UserAnswerId { get; set; } 
     
        public FilterExpression<int>? QuestionId { get; set; } 
     
        public FilterExpression<int>? UserExamId { get; set; } 
     
        public FilterExpression<int>? SectionId { get; set; } 
     }

    public class AddUserAnswerReqDto : IMapTo<UserAnswer>
    {
        
        public int QuestionId { get; set; }
        
        public int UserExamId { get; set; }
        
        public int SectionId { get; set; }
    }

    public class UpdateUserAnswerReqDto :AddUserAnswerReqDto, IMapTo<UserAnswer>
    {
        public int UserAnswerId { get; set; }
    }

}

