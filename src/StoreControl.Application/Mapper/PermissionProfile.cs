using AutoMapper;
using StoreControl.Application.Features.PermissionFeatures;
using StoreControl.Application.Features.PermissionFeatures.Commands.CreatePermission;
using StoreControl.Application.Features.PermissionFeatures.Commands.UpdatePermission;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Mapper
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionDto>();

            CreateMap<UpdatePermissionCommand, Permission>();

            CreateMap<CreatePermissionCommand, Permission>();
        }
    }
}
