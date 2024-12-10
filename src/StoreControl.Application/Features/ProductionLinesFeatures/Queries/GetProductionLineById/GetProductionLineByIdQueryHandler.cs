using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;
using StoreControl.Domain.Exceptions;

namespace StoreControl.Application.Features.ProductionLinesFeatures.Queries.GetProductionLineById
{
    public class GetProductionLineByIdQueryHandler : IRequestHandler<GetProductionLineByIdQuery, ProductionLineDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProductionLineByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductionLineDto> Handle(GetProductionLineByIdQuery request, CancellationToken cancellationToken)
        {
            var productionLine = await _dbContext.ProductionLines
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id ==  request.Id, cancellationToken);

            if (productionLine == null)
            {
                throw new NotFoundException("Production line not found.");
            }

            return _mapper.Map<ProductionLineDto>(productionLine);
        }
    }
}
