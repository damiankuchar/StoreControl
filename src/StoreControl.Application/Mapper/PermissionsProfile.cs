using AutoMapper;
using StoreControl.Application.Features.PermissionsFeatures;
using StoreControl.Application.Features.PermissionsFeatures.Commands.CreatePermission;
using StoreControl.Application.Features.PermissionsFeatures.Commands.UpdatePermission;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Mapper
{
    public class PermissionsProfile : Profile
    {
        public PermissionsProfile()
        {
            CreateMap<Permission, PermissionDto>();

            CreateMap<UpdatePermissionCommand, Permission>();

            CreateMap<CreatePermissionCommand, Permission>();
        }
    }
}
