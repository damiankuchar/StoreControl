using MediatR;
using System.Text.Json.Serialization;

namespace StoreControl.Application.Features.PermissionFeatures.Commands.UpdatePermission
{
    public class UpdatePermissionCommand : IRequest<PermissionDto>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
