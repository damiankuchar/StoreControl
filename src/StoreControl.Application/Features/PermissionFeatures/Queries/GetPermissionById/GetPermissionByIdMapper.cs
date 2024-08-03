using AutoMapper;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.PermissionFeatures.Queries.GetPermissionById
{
    public class GetPermissionByIdMapper : Profile
    {
        public GetPermissionByIdMapper()
        {
            CreateMap<Permission, GetPermissionByIdResponse>();
        }
    }
}
