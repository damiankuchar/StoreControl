using MediatR;

namespace StoreControl.Application.Features.PermissionFeatures.Queries.GetPermissionById
{
    public class GetPermissionByIdQuery: IRequest<PermissionDto>
    {
        public Guid Id { get; set; }
    }
}
