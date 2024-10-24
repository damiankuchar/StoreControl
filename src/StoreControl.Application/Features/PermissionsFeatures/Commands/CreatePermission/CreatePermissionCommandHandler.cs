using AutoMapper;
using MediatR;
using StoreControl.Application.Interfaces;
using StoreControl.Application.Shared.Services.PermissionService;
using StoreControl.Domain.Entities;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.PermissionsFeatures.Commands.CreatePermission
{
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, PermissionDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;

        public CreatePermissionCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IPermissionService permissionService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _permissionService = permissionService;
        }

        public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var permission = _mapper.Map<Permission>(request);

                var isPermissionUnique = await _permissionService.IsPermissionUniqueAsync(permission, cancellationToken);

                if (!isPermissionUnique)
                {
                    throw new BadRequestException("Permission with provided name already exists.");
                }

                await _dbContext.Permissions.AddAsync(permission, cancellationToken);
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
