using MediatR;

namespace StoreControl.Application.Features.ProductionLinesFeatures.Queries.GetAllProductionLines
{
    public class GetAllProductionLinesQuery : IRequest<IEnumerable<ProductionLineDto>>
    {
    }
}
