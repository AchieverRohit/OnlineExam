


namespace thinkschool.OnlineExam.Core.Validations
{
    // Validation class for AddSectionResultReqDto
    public class AddSectionResultReqDtoValidator : AbstractValidator<AddSectionResultReqDto>
    {
        public AddSectionResultReqDtoValidator()
        {
         
            RuleFor(x => x.SectionId).NotNull().WithMessage("SectionId is required.");
            RuleFor(x => x.UserExamId).NotNull().WithMessage("UserExamId is required.");
            RuleFor(x => x.QuestionsAttempted).NotNull().WithMessage("QuestionsAttempted is required.");
            RuleFor(x => x.MarksObtained).NotNull().WithMessage("MarksObtained is required.");
            RuleFor(x => x.ResultStatus).NotEmpty().WithMessage("ResultStatus is required.");
            RuleFor(x => x.ResultStatus).MaximumLength(255).WithMessage("ResultStatus cannot be longer than 255 characters.");
        }
    }

     // Validation class for updateSectionResultReqDto
    public class UpdateSectionResultReqDtoValidator : AbstractValidator<UpdateSectionResultReqDto>
    {
        public UpdateSectionResultReqDtoValidator()
        {           
            RuleFor(x => x.SectionResultId).NotNull().WithMessage("SectionResultId is required.");
            RuleFor(x => x.SectionId).NotNull().WithMessage("SectionId is required.");
            RuleFor(x => x.UserExamId).NotNull().WithMessage("UserExamId is required.");
            RuleFor(x => x.QuestionsAttempted).NotNull().WithMessage("QuestionsAttempted is required.");
            RuleFor(x => x.MarksObtained).NotNull().WithMessage("MarksObtained is required.");
            RuleFor(x => x.ResultStatus).NotEmpty().WithMessage("ResultStatus is required.");
            RuleFor(x => x.ResultStatus).MaximumLength(255).WithMessage("ResultStatus cannot be longer than 255 characters.");
        }
    }
}


