using MediatR;
using System.Text.Json;

namespace StoreControl.Application.Features.ProductionLinesFeatures.Commands.CreateProductionLine
{
    public class CreateProductionLineCommand : IRequest<ProductionLineDto>
    {
        public string Name { get; set; } = string.Empty;
        public JsonDocument CanvasData { get; set; } = null!;
    }
}
