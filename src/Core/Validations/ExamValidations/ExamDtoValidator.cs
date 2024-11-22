


namespace thinkschool.OnlineExam.Core.Validations
{
    // Validation class for AddExamReqDto
    public class AddExamReqDtoValidator : AbstractValidator<AddExamReqDto>
    {
        public AddExamReqDtoValidator()
        {
         
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(255).WithMessage("Title cannot be longer than 255 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Description cannot be longer than 255 characters.");
            RuleFor(x => x.StartDate).NotNull().WithMessage("StartDate is required.");
            RuleFor(x => x.EndDate).NotNull().WithMessage("EndDate is required.");
            RuleFor(x => x.Duration).NotNull().WithMessage("Duration is required.");
            RuleFor(x => x.TotalQuestions).NotNull().WithMessage("TotalQuestions is required.");
            RuleFor(x => x.TotalMarks).NotNull().WithMessage("TotalMarks is required.");
            RuleFor(x => x.PassingMarks).NotNull().WithMessage("PassingMarks is required.");
            RuleFor(x => x.IsRandomized).NotNull().WithMessage("IsRandomized is required.");
            RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }

     // Validation class for updateExamReqDto
    public class UpdateExamReqDtoValidator : AbstractValidator<UpdateExamReqDto>
    {
        public UpdateExamReqDtoValidator()
        {           
            RuleFor(x => x.ExamId).NotNull().WithMessage("ExamId is required.");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Title).MaximumLength(255).WithMessage("Title cannot be longer than 255 characters.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Description).MaximumLength(255).WithMessage("Description cannot be longer than 255 characters.");
            RuleFor(x => x.StartDate).NotNull().WithMessage("StartDate is required.");
            RuleFor(x => x.EndDate).NotNull().WithMessage("EndDate is required.");
            RuleFor(x => x.Duration).NotNull().WithMessage("Duration is required.");
            RuleFor(x => x.TotalQuestions).NotNull().WithMessage("TotalQuestions is required.");
            RuleFor(x => x.TotalMarks).NotNull().WithMessage("TotalMarks is required.");
            RuleFor(x => x.PassingMarks).NotNull().WithMessage("PassingMarks is required.");
            RuleFor(x => x.IsRandomized).NotNull().WithMessage("IsRandomized is required.");
            RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }

    public class ExamDtoValidator : AbstractValidator<ExamDto>
    {
        public ExamDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(255).WithMessage("Description cannot exceed 255 characters.");

            RuleFor(x => x.StartDate)
                .Must((dto, startDate) => startDate < dto.EndDate).WithMessage("Start date must be before the end date.");

            RuleFor(x => x.TotalMarks)
                .GreaterThan(0).WithMessage("Total marks must be greater than zero.");

            RuleFor(x => x.PassingMarks)
                .GreaterThan(0).WithMessage("Passing marks must be greater than zero.")
                .LessThanOrEqualTo(x => x.TotalMarks).WithMessage("Passing marks must be less than or equal to total marks.");
        }
    }
}


