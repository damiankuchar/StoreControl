using FluentValidation;

namespace StoreControl.Application.Features.PermissionFeatures.Commands.CreatePermission
{
    public class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
    {
        public CreatePermissionCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Permission name is required.");
        }
    }
}
