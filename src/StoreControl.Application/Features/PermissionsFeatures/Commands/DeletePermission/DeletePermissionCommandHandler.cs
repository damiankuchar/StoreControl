using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.PermissionsFeatures.Commands.DeletePermission
{
    public class DeletePermissionCommandHandler : IRequestHandler<DeletePermissionCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeletePermissionCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var permission = await _dbContext.Permissions
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (permission == null)
                {
                    throw new NotFoundException("Permission do not exist.");
                }

                _dbContext.Permissions.Remove(permission);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
