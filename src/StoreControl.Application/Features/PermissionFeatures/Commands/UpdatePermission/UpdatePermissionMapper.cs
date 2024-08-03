using AutoMapper;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.PermissionFeatures.Commands.UpdatePermission
{
    public class UpdatePermissionMapper : Profile
    {
        public UpdatePermissionMapper()
        {
            CreateMap<UpdatePermissionCommand, Permission>();
        }
    }
}
