


namespace thinkschool.OnlineExam.Core.Validations
{
    // Validation class for AddQuestionReqDto
    public class AddQuestionReqDtoValidator : AbstractValidator<AddQuestionReqDto>
    {
        public AddQuestionReqDtoValidator()
        {
         
            RuleFor(x => x.SectionId).NotNull().WithMessage("SectionId is required.");
            RuleFor(x => x.QuestionText).NotEmpty().WithMessage("QuestionText is required.");
            RuleFor(x => x.QuestionText).MaximumLength(255).WithMessage("QuestionText cannot be longer than 255 characters.");
            RuleFor(x => x.IsMedia).NotNull().WithMessage("IsMedia is required.");
            RuleFor(x => x.MediaType).NotEmpty().WithMessage("MediaType is required.");
            RuleFor(x => x.MediaType).MaximumLength(255).WithMessage("MediaType cannot be longer than 255 characters.");
            RuleFor(x => x.MediaURL).NotEmpty().WithMessage("MediaURL is required.");
            RuleFor(x => x.MediaURL).MaximumLength(255).WithMessage("MediaURL cannot be longer than 255 characters.");
            RuleFor(x => x.IsMultipleChoice).NotNull().WithMessage("IsMultipleChoice is required.");
            RuleFor(x => x.IsFromQuestionBank).NotNull().WithMessage("IsFromQuestionBank is required.");
            RuleFor(x => x.QuestionMaxMarks).NotNull().WithMessage("QuestionMaxMarks is required.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }

     // Validation class for updateQuestionReqDto
    public class UpdateQuestionReqDtoValidator : AbstractValidator<UpdateQuestionReqDto>
    {
        public UpdateQuestionReqDtoValidator()
        {           
            RuleFor(x => x.QuestionId).NotNull().WithMessage("QuestionId is required.");
            RuleFor(x => x.SectionId).NotNull().WithMessage("SectionId is required.");
            RuleFor(x => x.QuestionText).NotEmpty().WithMessage("QuestionText is required.");
            RuleFor(x => x.QuestionText).MaximumLength(255).WithMessage("QuestionText cannot be longer than 255 characters.");
            RuleFor(x => x.IsMedia).NotNull().WithMessage("IsMedia is required.");
            RuleFor(x => x.MediaType).NotEmpty().WithMessage("MediaType is required.");
            RuleFor(x => x.MediaType).MaximumLength(255).WithMessage("MediaType cannot be longer than 255 characters.");
            RuleFor(x => x.MediaURL).NotEmpty().WithMessage("MediaURL is required.");
            RuleFor(x => x.MediaURL).MaximumLength(255).WithMessage("MediaURL cannot be longer than 255 characters.");
            RuleFor(x => x.IsMultipleChoice).NotNull().WithMessage("IsMultipleChoice is required.");
            RuleFor(x => x.IsFromQuestionBank).NotNull().WithMessage("IsFromQuestionBank is required.");
            RuleFor(x => x.QuestionMaxMarks).NotNull().WithMessage("QuestionMaxMarks is required.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }
}


