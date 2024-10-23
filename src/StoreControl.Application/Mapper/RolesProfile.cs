using AutoMapper;
using StoreControl.Application.Features.RolesFeatures;
using StoreControl.Application.Features.RolesFeatures.Commands.CreateRole;
using StoreControl.Application.Features.RolesFeatures.Commands.UpdateRole;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Mapper
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<Role, RoleDetailedDto>();

            CreateMap<Permission, RolePermissionDto>();

            CreateMap<CreateRoleCommand, Role>();

            CreateMap<UpdateRoleCommand, Role>();
        }
    }
}
