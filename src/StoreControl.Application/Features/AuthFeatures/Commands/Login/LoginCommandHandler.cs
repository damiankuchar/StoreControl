using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.AuthFeatures.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandler(
            IApplicationDbContext dbContext,
            IPasswordHasher<User> passwordHasher,
            IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);
            
            try
            {
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(x => x.Username == request.Login || x.Email == request.Login, cancellationToken);

                if (user == null)
                {
                    throw new BadRequestException("Invalid credentials.");
                }

                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

                if (passwordVerificationResult == PasswordVerificationResult.Failed)
                {
                    throw new BadRequestException("Invalid credentials.");
                }

                var token = await _jwtProvider.GenerateAccessTokenAsync(user, cancellationToken);
                var refreshToken = await _jwtProvider.GenerateAndSaveRefreshTokenAsync(user, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new LoginResponse
                {
                    Token = token,
                    RefreshToken = refreshToken,
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
