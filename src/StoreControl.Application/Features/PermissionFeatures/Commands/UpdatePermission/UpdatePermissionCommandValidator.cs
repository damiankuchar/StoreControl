﻿using FluentValidation;

namespace StoreControl.Application.Features.PermissionFeatures.Commands.UpdatePermission
{
    public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Permission name is required.");
        }
    }
}
