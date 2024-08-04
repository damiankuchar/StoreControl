using AutoMapper;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.RoleFeatures.Queries.GetAllRoles
{
    public class GetAllRolesMapper : Profile
    {
        public GetAllRolesMapper()
        {
            CreateMap<Role, GetAllRolesResponse>();
        }
    }
}
