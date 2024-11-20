
namespace thinkschool.OnlineExam.Core.Models
{

    public class GetAllUserAnswerOptionReqDto : BasePagination
    {     
     
        public FilterExpression<int>? UserAnswerId { get; set; } 
     
        public FilterExpression<int>? OptionId { get; set; } 
     }

    public class AddUserAnswerOptionReqDto : IMapTo<UserAnswerOption>
    {
    }

    public class UpdateUserAnswerOptionReqDto :AddUserAnswerOptionReqDto, IMapTo<UserAnswerOption>
    {
        public int UserAnswerId { get; set; }
       public int OptionId { get; set; }
    }

}

