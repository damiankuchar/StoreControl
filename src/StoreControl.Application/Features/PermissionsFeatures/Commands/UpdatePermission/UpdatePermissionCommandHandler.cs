﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Application.Shared.Services.PermissionService;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.PermissionsFeatures.Commands.UpdatePermission
{
    public class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;

        public UpdatePermissionCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IPermissionService permissionService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _permissionService = permissionService;
        }

        public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var permission = await _dbContext.Permissions
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (permission == null)
                {
                    throw new NotFoundException("Permission do not exist");
                }

                _mapper.Map(request, permission);

                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return _mapper.Map<PermissionDto>(permission);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
