using MediatR;

namespace StoreControl.Application.Features.RoleFeatures.Commands.DeleteRole
{
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
