﻿using MediatR;

namespace StoreControl.Application.Features.AuthFeatures.Commands.Refresh
{
    public class RefreshCommand : IRequest<RefreshResponse>
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}