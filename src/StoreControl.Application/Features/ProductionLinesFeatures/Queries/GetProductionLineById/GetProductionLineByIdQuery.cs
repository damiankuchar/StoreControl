using MediatR;

namespace StoreControl.Application.Features.ProductionLinesFeatures.Queries.GetProductionLineById
{
    public class GetProductionLineByIdQuery : IRequest<ProductionLineDto>
    {
        public Guid Id { get; set; }
    }
}
