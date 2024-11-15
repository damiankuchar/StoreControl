using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.UsersFeatures.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDetailedDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDetailedDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (user == null)
                {
                    throw new NotFoundException("User do not exist.");
                }

                var roles = await _dbContext.Roles
                    .Where(x => request.RoleIds.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                user.Roles.Clear();
                
                foreach (var role in roles)
                {
                    user.Roles.Add(role);
                }

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
    }
}
