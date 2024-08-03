using AutoMapper;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.PermissionFeatures.Commands.CreatePermission
{
    public class CreatePermissionMapper : Profile
    {
        public CreatePermissionMapper()
        {
            CreateMap<CreatePermissionCommand, Permission>();
        }
    }
}
