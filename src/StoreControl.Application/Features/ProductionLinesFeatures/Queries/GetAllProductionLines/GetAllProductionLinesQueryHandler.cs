using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreControl.Application.Interfaces;

namespace StoreControl.Application.Features.ProductionLinesFeatures.Queries.GetAllProductionLines
{
    public class GetAllProductionLinesQueryHandler : IRequestHandler<GetAllProductionLinesQuery, IEnumerable<ProductionLineDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllProductionLinesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductionLineDto>> Handle(GetAllProductionLinesQuery request, CancellationToken cancellationToken)
        {
            var productionLines = await _dbContext.ProductionLines
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ProductionLineDto>>(productionLines);
        }
    }
}
