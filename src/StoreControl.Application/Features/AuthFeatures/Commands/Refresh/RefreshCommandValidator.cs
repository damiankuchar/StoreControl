using FluentValidation;

namespace StoreControl.Application.Features.AuthFeatures.Commands.Refresh
{
    public class RefreshCommandValidator : AbstractValidator<RefreshCommand>
    {
        public RefreshCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Access token is not provided.");

            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token is not provided.");
        }
    }
}
