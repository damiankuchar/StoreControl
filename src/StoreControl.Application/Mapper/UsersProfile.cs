using AutoMapper;
using StoreControl.Application.Features.UsersFeatures;
using StoreControl.Application.Features.UsersFeatures.Commands.CreateUser;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Mapper
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<User, UserDetailedDto>();

            CreateMap<Role, UserRoleDto>();

            CreateMap<CreateUserCommand, User>();
        }
    }
}
