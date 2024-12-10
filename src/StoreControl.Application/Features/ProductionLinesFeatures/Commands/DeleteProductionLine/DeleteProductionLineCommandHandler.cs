using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.ProductionLinesFeatures.Commands.DeleteProductionLine
{
    public class DeleteProductionLineCommandHandler : IRequestHandler<DeleteProductionLineCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteProductionLineCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteProductionLineCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var productionLine = await _dbContext.ProductionLines
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (productionLine == null)
                {
                    throw new NotFoundException("Production line do not exist.");
                }

                _dbContext.ProductionLines.Remove(productionLine);
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
