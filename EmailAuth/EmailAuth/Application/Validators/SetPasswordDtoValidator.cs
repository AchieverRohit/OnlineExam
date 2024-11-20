using EmailAuth.Application.DTOs;
using EmailAuth.Common.Constants;
using FluentValidation;
using System.Text.RegularExpressions;

namespace EmailAuth.Application.Validators
{
    /// <summary>
    /// Validator for <see cref="SetPasswordDto"/>.
    /// </summary>
    internal class SetPasswordDtoValidator : AbstractValidator<SetPasswordDto>
    {
        public SetPasswordDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().WithMessage(Constants.InvalidEmailVal);

            RuleFor(x => x.OTP)
                .NotEmpty().WithMessage(Constants.ReqOTPVal);

            RuleFor(x => x.Password.Trim())
                .NotEmpty().WithMessage(Constants.ReqPasswordVal)
                .MinimumLength(8).WithMessage(Constants.PasswordLengthVal)
                .MaximumLength(128).WithMessage(Constants.PasswordLongVal)
                .Matches(new Regex(Constants.PasswordRegex))
                .WithMessage(Constants.InvalidPasswordVal);
        }
    }
}
