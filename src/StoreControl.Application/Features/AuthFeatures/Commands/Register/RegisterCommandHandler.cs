using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StoreControl.Application.Interfaces;
using StoreControl.Application.Shared.Services.UserService;
using StoreControl.Domain.Entities;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.AuthFeatures.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserService _userService;

        public RegisterCommandHandler(
            IApplicationDbContext dbContext,
            IMapper mapper,
            IPasswordHasher<User> passwordHasher,
            IJwtProvider jwtProvider,
            IUserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _userService = userService;
        }

        public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var user = _mapper.Map<User>(request);

                var isUserUnique = await _userService.IsUserUniqueAsync(user, cancellationToken);

                if (!isUserUnique)
                {
                    throw new BadRequestException("User with provided credentials already exists.");
                }

                user.Password = _passwordHasher.HashPassword(user, request.Password);

                await _dbContext.Users.AddAsync(user, cancellationToken);

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
