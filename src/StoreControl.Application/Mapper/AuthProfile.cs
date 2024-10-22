using AutoMapper;
using StoreControl.Application.Features.AuthFeatures.Commands.Register;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Mapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterCommand, User>();
        }
    }
}
