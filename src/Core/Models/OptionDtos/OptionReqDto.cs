
namespace thinkschool.OnlineExam.Core.Models
{

    public class GetAllOptionReqDto : BasePagination
    {     
     
        public FilterExpression<int>? OptionId { get; set; } 
     
        public FilterExpression<int>? QuestionId { get; set; } 
     
        public FilterExpression<string>? OptionText { get; set; } 
     
        public FilterExpression<bool>? IsCorrect { get; set; } 
     
        public FilterExpression<decimal>? Marks { get; set; } 
     }

    public class AddOptionReqDto : IMapTo<Option>
    {
        
        public int QuestionId { get; set; }
        
        public string OptionText { get; set; }
        
        public bool IsCorrect { get; set; }
        
        public decimal Marks { get; set; }
    }

    public class UpdateOptionReqDto :AddOptionReqDto, IMapTo<Option>
    {
        public int OptionId { get; set; }
    }

}

