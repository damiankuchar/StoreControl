using AutoMapper;
using StoreControl.Application.Features.RoleFeatures;
using StoreControl.Application.Features.RoleFeatures.Commands.CreateRole;
using StoreControl.Application.Features.RoleFeatures.Commands.UpdateRole;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Mapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<Role, RoleDetailedDto>();

            CreateMap<Permission, RolePermissionDto>();

            CreateMap<CreateRoleCommand, Role>();

            CreateMap<UpdateRoleCommand, Role>();
        }
    }
}
