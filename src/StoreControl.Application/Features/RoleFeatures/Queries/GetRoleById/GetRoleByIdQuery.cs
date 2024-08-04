using MediatR;

namespace StoreControl.Application.Features.RoleFeatures.Queries.GetRoleById
{
    public class GetRoleByIdQuery : IRequest<GetRoleByIdResponse>
    {
        public Guid Id { get; set; }
    }
}
