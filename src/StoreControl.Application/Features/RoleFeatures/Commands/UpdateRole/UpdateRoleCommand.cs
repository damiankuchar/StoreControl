using MediatR;
using System.Text.Json.Serialization;

namespace StoreControl.Application.Features.RoleFeatures.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Guid> PermissionIds { get; set; } = new List<Guid>();
    }
}
