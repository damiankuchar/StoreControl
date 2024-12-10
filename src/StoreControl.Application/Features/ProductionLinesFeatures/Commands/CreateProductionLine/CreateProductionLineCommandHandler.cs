using AutoMapper;
using MediatR;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Entities;

namespace StoreControl.Application.Features.ProductionLinesFeatures.Commands.CreateProductionLine
{
    public class CreateProductionLineCommandHandler : IRequestHandler<CreateProductionLineCommand, ProductionLineDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateProductionLineCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductionLineDto> Handle(CreateProductionLineCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

            try
            {
                var productionLine = _mapper.Map<ProductionLine>(request);

                await _dbContext.ProductionLines.AddAsync(productionLine, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                return _mapper.Map<ProductionLineDto>(productionLine);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
