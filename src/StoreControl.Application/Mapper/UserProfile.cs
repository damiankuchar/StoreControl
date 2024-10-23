using AutoMapper;
using StoreControl.Application.Features.UserFeatures;
using StoreControl.Application.Features.UserFeatures.Commands.CreateUser;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<User, UserDetailedDto>();

            CreateMap<Role, UserRoleDto>();

            CreateMap<CreateUserCommand, User>();
        }
    }
}
