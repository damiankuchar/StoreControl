using AutoMapper;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.PermissionFeatures.Queries.GetAllPermisions
{
    public class GetAllPermisionsMapper : Profile
    {
        public GetAllPermisionsMapper()
        {
            CreateMap<Permission, GetAllPermisionsResponse>();
        }
    }
}
