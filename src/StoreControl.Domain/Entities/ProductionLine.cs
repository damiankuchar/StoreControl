using StoreControl.Domain.Common;
using System.Text.Json;

namespace StoreControl.Domain.Entities
{
    public class ProductionLine : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public JsonDocument CanvasData { get; set; } = null!;
    }
}
