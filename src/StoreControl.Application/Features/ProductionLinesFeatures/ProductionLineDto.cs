using System.Text.Json;

namespace StoreControl.Application.Features.ProductionLinesFeatures
{
    public class ProductionLineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public JsonDocument CanvasData { get; set; } = null!;
    }
}
