


namespace thinkschool.OnlineExam.Core.Validations
{
    // Validation class for AddUserExamReqDto
    public class AddUserExamReqDtoValidator : AbstractValidator<AddUserExamReqDto>
    {
        public AddUserExamReqDtoValidator()
        {
         
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.UserId).MaximumLength(450).WithMessage("UserId cannot be longer than 450 characters.");
            RuleFor(x => x.ExamId).NotNull().WithMessage("ExamId is required.");
            RuleFor(x => x.StartedOn).NotNull().WithMessage("StartedOn is required.");
            RuleFor(x => x.ExamStatus).NotEmpty().WithMessage("ExamStatus is required.");
            RuleFor(x => x.ExamStatus).MaximumLength(255).WithMessage("ExamStatus cannot be longer than 255 characters.");
            RuleFor(x => x.NoOfAttempt).NotNull().WithMessage("NoOfAttempt is required.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }

     // Validation class for updateUserExamReqDto
    public class UpdateUserExamReqDtoValidator : AbstractValidator<UpdateUserExamReqDto>
    {
        public UpdateUserExamReqDtoValidator()
        {           
            RuleFor(x => x.UserExamId).NotNull().WithMessage("UserExamId is required.");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.UserId).MaximumLength(450).WithMessage("UserId cannot be longer than 450 characters.");
            RuleFor(x => x.ExamId).NotNull().WithMessage("ExamId is required.");
            RuleFor(x => x.StartedOn).NotNull().WithMessage("StartedOn is required.");
            RuleFor(x => x.ExamStatus).NotEmpty().WithMessage("ExamStatus is required.");
            RuleFor(x => x.ExamStatus).MaximumLength(255).WithMessage("ExamStatus cannot be longer than 255 characters.");
            RuleFor(x => x.NoOfAttempt).NotNull().WithMessage("NoOfAttempt is required.");
            RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required.");
            RuleFor(x => x.CreatedBy).MaximumLength(255).WithMessage("CreatedBy cannot be longer than 255 characters.");
            RuleFor(x => x.CreatedOn).NotNull().WithMessage("CreatedOn is required.");
            RuleFor(x => x.UpdatedOn).NotNull().WithMessage("UpdatedOn is required.");
        }
    }
}


