using MediatR;

namespace StoreControl.Application.Features.PermissionFeatures.Commands.DeletePermission
{
    public class DeletePermissionCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
