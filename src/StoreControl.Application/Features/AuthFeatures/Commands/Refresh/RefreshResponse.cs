﻿namespace StoreControl.Application.Features.AuthFeatures.Commands.Refresh
{
    public class RefreshResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
