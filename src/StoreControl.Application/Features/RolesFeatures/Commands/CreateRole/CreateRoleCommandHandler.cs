using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.RolesFeatures.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDetailedDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<RoleDetailedDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                await IsRoleUnique(request.Name, cancellationToken);

                var role = _mapper.Map<Role>(request);

                var permissions = await _dbContext.Permissions
                    .Where(x => request.PermissionIds.Contains(x.Id))
                    .ToListAsync(cancellationToken);

                role.Permissions = permissions;

                await _dbContext.Roles.AddAsync(role, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return _mapper.Map<RoleDetailedDto>(role);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private async Task IsRoleUnique(string roleName, CancellationToken cancellationToken)
        {
            var roleAlreadyExists = await _dbContext.Roles
                .AnyAsync(x => x.Name == roleName, cancellationToken);

            if (roleAlreadyExists)
            {
                throw new BadRequestException("Role with provided name already exists.");
            }
        }
    }
}
