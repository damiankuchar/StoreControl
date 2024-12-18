﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Constants;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.AuthFeatures.Commands.Refresh
{
    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, AuthResponseDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserClaimsService _userClaimsService;

        public RefreshCommandHandler(IApplicationDbContext dbContext, IJwtProvider jwtProvider, IUserClaimsService userClaimsService)
        {
            _dbContext = dbContext;
            _jwtProvider = jwtProvider;
            _userClaimsService = userClaimsService;
        }

        public async Task<AuthResponseDto> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var claims = _userClaimsService.GetClaimsFromExpiredToken(request.Token);
                claims.TryGetValue(CustomClaims.UserId, out var subClaimValue);

                if (!Guid.TryParse(subClaimValue, out Guid userId))
                {
                    throw new BadRequestException("Invalid token.");
                }

                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

                if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    throw new BadRequestException("Refresh token is not valid.");
                }

                var token = await _jwtProvider.GenerateAccessTokenAsync(user, cancellationToken);
                var refreshToken = await _jwtProvider.GenerateAndSaveRefreshTokenAsync(user, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new AuthResponseDto
                {
                    Token = token,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
