using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.AuthFeatures.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public RegisterCommandHandler(
            IApplicationDbContext dbContext,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                await IsUserUnique(request, cancellationToken);

                var user = _mapper.Map<User>(request);

                user.Password = _passwordHasher.HashPassword(user, request.Password);

                await _dbContext.Users.AddAsync(user, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var token = await _jwtProvider.GenerateAccessTokenAsync(user, cancellationToken);
                var refreshToken = await _jwtProvider.GenerateAndSaveRefreshTokenAsync(user, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return new RegisterResponse
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

        private async Task IsUserUnique(RegisterCommand request, CancellationToken cancellationToken)
        {
            var isUserAlreadyRegistered = await _dbContext.Users
                .AnyAsync(x => x.Email == request.Email || x.Username == request.Username, cancellationToken);

            if (isUserAlreadyRegistered)
            {
                throw new BadRequestException("User with provided credentials already exists.");
            }
        }
    }
}
