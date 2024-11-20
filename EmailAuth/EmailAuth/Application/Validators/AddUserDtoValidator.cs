using EmailAuth.Application.DTOs;
using EmailAuth.Common.Constants;
using FluentValidation;

namespace EmailAuth.Application.Validators
{
    /// <summary>
    /// Validator for <see cref="AddUserDto"/>.
    /// </summary>
    internal class AddUserDtoValidator : AbstractValidator<AddUserDto>
    {
        public AddUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(320)
                .EmailAddress()
                .WithMessage(Constants.InvalidEmailVal);
        }
    }
}
