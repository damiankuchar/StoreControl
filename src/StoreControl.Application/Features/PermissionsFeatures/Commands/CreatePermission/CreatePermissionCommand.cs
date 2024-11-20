using MediatR;

namespace StoreControl.Application.Features.PermissionsFeatures.Commands.CreatePermission
{
    public class CreatePermissionCommand : IRequest<PermissionDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
