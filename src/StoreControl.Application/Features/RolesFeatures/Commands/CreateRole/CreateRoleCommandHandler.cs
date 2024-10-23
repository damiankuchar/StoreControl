using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Application.Shared.Services.RoleService;
using StoreControl.Domain.Entities;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.RolesFeatures.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDetailedDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public CreateRoleCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IRoleService roleService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<RoleDetailedDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var role = _mapper.Map<Role>(request);

                var isRoleUnique = await _roleService.IsRoleUniqueAsync(role, cancellationToken);

                if (!isRoleUnique)
                {
                    throw new BadRequestException("Role with provided name already exists.");
                }

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
    }
}
