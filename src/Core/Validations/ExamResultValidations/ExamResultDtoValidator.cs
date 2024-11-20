


namespace thinkschool.OnlineExam.Core.Validations
{
    // Validation class for AddExamResultReqDto
    public class AddExamResultReqDtoValidator : AbstractValidator<AddExamResultReqDto>
    {
        public AddExamResultReqDtoValidator()
        {
         
            RuleFor(x => x.UserExamId).NotNull().WithMessage("UserExamId is required.");
            RuleFor(x => x.TotalObtainedMarks).NotNull().WithMessage("TotalObtainedMarks is required.");
            RuleFor(x => x.ResultStatus).NotEmpty().WithMessage("ResultStatus is required.");
            RuleFor(x => x.ResultStatus).MaximumLength(255).WithMessage("ResultStatus cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }

     // Validation class for updateExamResultReqDto
    public class UpdateExamResultReqDtoValidator : AbstractValidator<UpdateExamResultReqDto>
    {
        public UpdateExamResultReqDtoValidator()
        {           
            RuleFor(x => x.ExamResultId).NotNull().WithMessage("ExamResultId is required.");
            RuleFor(x => x.UserExamId).NotNull().WithMessage("UserExamId is required.");
            RuleFor(x => x.TotalObtainedMarks).NotNull().WithMessage("TotalObtainedMarks is required.");
            RuleFor(x => x.ResultStatus).NotEmpty().WithMessage("ResultStatus is required.");
            RuleFor(x => x.ResultStatus).MaximumLength(255).WithMessage("ResultStatus cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }
}


