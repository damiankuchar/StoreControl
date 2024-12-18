﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Application.Shared.Services.RoleService;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.RolesFeatures.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDetailedDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public UpdateRoleCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IRoleService roleService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<RoleDetailedDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var role = await _dbContext.Roles
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (role == null)
                {
                    throw new NotFoundException("Role do not exist.");
                }

                _mapper.Map(request, role);

                var permissions = await _dbContext.Permissions
                    .Where(x => request.PermissionIds.Contains(x.Id))
                    .ToListAsync();

                role.Permissions.Clear();
                role.Permissions = permissions;

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
