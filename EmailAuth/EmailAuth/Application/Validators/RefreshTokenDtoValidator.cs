using EmailAuth.Application.DTOs;
using EmailAuth.Common.Constants;
using FluentValidation;

namespace EmailAuth.Application.Validators
{
    /// <summary>
    /// Validator for <see cref="RefreshTokenDto"/>.
    /// </summary>
    internal class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenDtoValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty().WithMessage(Constants.ReqAccessTokenVal);
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage(Constants.ReqRefreshTokenVal);
        }
    }
}
