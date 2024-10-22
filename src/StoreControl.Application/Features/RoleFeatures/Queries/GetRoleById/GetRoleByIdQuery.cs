using MediatR;

namespace StoreControl.Application.Features.RoleFeatures.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<RoleDetailedDto>
    {
        public Guid Id { get; set; }
    }
}
