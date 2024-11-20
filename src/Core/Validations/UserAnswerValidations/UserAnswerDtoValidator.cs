


namespace thinkschool.OnlineExam.Core.Validations
{
    // Validation class for AddUserAnswerReqDto
    public class AddUserAnswerReqDtoValidator : AbstractValidator<AddUserAnswerReqDto>
    {
        public AddUserAnswerReqDtoValidator()
        {
         
            RuleFor(x => x.QuestionId).NotNull().WithMessage("QuestionId is required.");
            RuleFor(x => x.UserExamId).NotNull().WithMessage("UserExamId is required.");
            RuleFor(x => x.SectionId).NotNull().WithMessage("SectionId is required.");
        }
    }

     // Validation class for updateUserAnswerReqDto
    public class UpdateUserAnswerReqDtoValidator : AbstractValidator<UpdateUserAnswerReqDto>
    {
        public UpdateUserAnswerReqDtoValidator()
        {           
            RuleFor(x => x.UserAnswerId).NotNull().WithMessage("UserAnswerId is required.");
            RuleFor(x => x.QuestionId).NotNull().WithMessage("QuestionId is required.");
            RuleFor(x => x.UserExamId).NotNull().WithMessage("UserExamId is required.");
            RuleFor(x => x.SectionId).NotNull().WithMessage("SectionId is required.");
        }
    }
}


