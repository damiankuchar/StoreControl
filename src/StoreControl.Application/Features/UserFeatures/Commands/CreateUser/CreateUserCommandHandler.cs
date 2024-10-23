using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.UserFeatures.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDetailedDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CreateUserCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDetailedDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var user = _mapper.Map<User>(request);

                await IsUserUnique(user, cancellationToken);

                var roles = await _dbContext.Roles
                    .Where(x => request.RoleIds.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                user.Roles = roles;
                user.Password = _passwordHasher.HashPassword(user, request.Password);

                await _dbContext.Users.AddAsync(user, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return _mapper.Map<UserDetailedDto>(user);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private async Task IsUserUnique(User user, CancellationToken cancellationToken)
        {
            var isUserAlreadyRegistered = await _dbContext.Users
                .AnyAsync(x => x.Email == user.Email || x.Username == user.Username, cancellationToken);

            if (isUserAlreadyRegistered)
            {
                throw new BadRequestException("User with provided credentials already exists.");
            }
        }
    }
}
