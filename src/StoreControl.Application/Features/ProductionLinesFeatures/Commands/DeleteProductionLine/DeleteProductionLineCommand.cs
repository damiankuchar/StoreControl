using MediatR;

namespace StoreControl.Application.Features.ProductionLinesFeatures.Commands.DeleteProductionLine
{
    public class DeleteProductionLineCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
