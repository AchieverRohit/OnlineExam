


namespace thinkschool.OnlineExam.Core.Validations
{
    // Validation class for AddSectionReqDto
    public class AddSectionReqDtoValidator : AbstractValidator<AddSectionReqDto>
    {
        public AddSectionReqDtoValidator()
        {
         
            RuleFor(x => x.ExamId).NotNull().WithMessage("ExamId is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(255).WithMessage("Title cannot be longer than 255 characters.");
            RuleFor(x => x.TotalQuestions).NotNull().WithMessage("TotalQuestions is required.");
            RuleFor(x => x.TotalMarks).NotNull().WithMessage("TotalMarks is required.");
            RuleFor(x => x.PassingMarks).NotNull().WithMessage("PassingMarks is required.");
            RuleFor(x => x.WeightagePercentage).NotNull().WithMessage("WeightagePercentage is required.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }

     // Validation class for updateSectionReqDto
    public class UpdateSectionReqDtoValidator : AbstractValidator<UpdateSectionReqDto>
    {
        public UpdateSectionReqDtoValidator()
        {           
            RuleFor(x => x.SectionId).NotNull().WithMessage("SectionId is required.");
            RuleFor(x => x.ExamId).NotNull().WithMessage("ExamId is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(255).WithMessage("Title cannot be longer than 255 characters.");
            RuleFor(x => x.TotalQuestions).NotNull().WithMessage("TotalQuestions is required.");
            RuleFor(x => x.TotalMarks).NotNull().WithMessage("TotalMarks is required.");
            RuleFor(x => x.PassingMarks).NotNull().WithMessage("PassingMarks is required.");
            RuleFor(x => x.WeightagePercentage).NotNull().WithMessage("WeightagePercentage is required.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }
}


