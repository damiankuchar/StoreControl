using MediatR;

namespace StoreControl.Application.Features.RolesFeatures.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<RoleDetailedDto>
    {
        public Guid Id { get; set; }
    }
}
