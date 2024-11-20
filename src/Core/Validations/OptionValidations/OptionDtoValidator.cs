


namespace thinkschool.OnlineExam.Core.Validations
{
    // Validation class for AddOptionReqDto
    public class AddOptionReqDtoValidator : AbstractValidator<AddOptionReqDto>
    {
        public AddOptionReqDtoValidator()
        {
         
            RuleFor(x => x.QuestionId).NotNull().WithMessage("QuestionId is required.");
            RuleFor(x => x.OptionText).NotEmpty().WithMessage("OptionText is required.");
            RuleFor(x => x.OptionText).MaximumLength(255).WithMessage("OptionText cannot be longer than 255 characters.");
            RuleFor(x => x.IsCorrect).NotNull().WithMessage("IsCorrect is required.");
            RuleFor(x => x.Marks).NotNull().WithMessage("Marks is required.");
        }
    }

     // Validation class for updateOptionReqDto
    public class UpdateOptionReqDtoValidator : AbstractValidator<UpdateOptionReqDto>
    {
        public UpdateOptionReqDtoValidator()
        {           
            RuleFor(x => x.OptionId).NotNull().WithMessage("OptionId is required.");
            RuleFor(x => x.QuestionId).NotNull().WithMessage("QuestionId is required.");
            RuleFor(x => x.OptionText).NotEmpty().WithMessage("OptionText is required.");
            RuleFor(x => x.OptionText).MaximumLength(255).WithMessage("OptionText cannot be longer than 255 characters.");
            RuleFor(x => x.IsCorrect).NotNull().WithMessage("IsCorrect is required.");
            RuleFor(x => x.Marks).NotNull().WithMessage("Marks is required.");
        }
    }
}


