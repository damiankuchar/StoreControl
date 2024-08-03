using AutoMapper;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.AuthFeatures.Commands.Register
{
    public class RegisterCommandMapper : Profile
    {
        public RegisterCommandMapper()
        {
            CreateMap<RegisterCommand, User>();
        }
    }
}
