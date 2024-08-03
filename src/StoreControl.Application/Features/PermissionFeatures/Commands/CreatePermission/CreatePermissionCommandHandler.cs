using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.PermissionFeatures.Commands.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreatePermissionCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                await IsPermissionUnique(request.Name, cancellationToken);

                var permission = _mapper.Map<Permission>(request);

                await _dbContext.Permissions.AddAsync(permission, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return permission.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private async Task IsPermissionUnique(string permissionName, CancellationToken cancellationToken)
        {
            var permissionAlreadyExists = await _dbContext.Permissions
                .AnyAsync(x => x.Name == permissionName, cancellationToken);

            if (permissionAlreadyExists)
            {
                throw new BadRequestException("Permission with provided name already exists.");
            }
        }
    }
}
