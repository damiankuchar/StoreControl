﻿using MediatR;

namespace StoreControl.Application.Features.PermissionFeatures.Commands.CreatePermission
{
    public class CreatePermissionCommand : IRequest<PermissionDto>
    {
        public string Name { get; set; } = string.Empty;
    }
}