using AutoMapper;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.RoleFeatures.Commands.CreateRole
{
    public class CreateRoleMapper : Profile
    {
        public CreateRoleMapper()
        {
            CreateMap<CreateRoleCommand, Role>();
        }
    }
}
