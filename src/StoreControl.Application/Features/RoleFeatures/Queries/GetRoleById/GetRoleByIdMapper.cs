using AutoMapper;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.RoleFeatures.Queries.GetRoleById
{
    public class GetRoleByIdMapper : Profile
    {
        public GetRoleByIdMapper()
        {
            CreateMap<Role, GetRoleByIdResponse>();

            CreateMap<Permission, RoleResponsePermission>();
        }
    }
}
