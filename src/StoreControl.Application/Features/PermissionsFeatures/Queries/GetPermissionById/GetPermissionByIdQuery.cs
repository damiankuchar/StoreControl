using MediatR;

namespace StoreControl.Application.Features.PermissionsFeatures.Queries.GetPermissionById
{
    public class GetPermissionByIdQuery: IRequest<PermissionDto>
    {
        public Guid Id { get; set; }
    }
}
