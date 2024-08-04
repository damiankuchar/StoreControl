using AutoMapper;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.RoleFeatures.Commands.UpdateRole
{
    public class UpdateRoleMapper : Profile
    {
        public UpdateRoleMapper()
        {
            CreateMap<UpdateRoleCommand, Role>();
        }
    }
}
