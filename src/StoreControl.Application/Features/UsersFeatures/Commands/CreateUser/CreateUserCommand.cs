﻿using MediatR;

namespace StoreControl.Application.Features.UsersFeatures.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserDetailedDto>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public List<Guid> RoleIds { get; set; } = new List<Guid>();
    }
}
