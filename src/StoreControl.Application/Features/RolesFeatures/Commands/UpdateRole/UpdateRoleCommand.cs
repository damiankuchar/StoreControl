using MediatR;
using System.Text.Json.Serialization;

namespace StoreControl.Application.Features.RolesFeatures.Commands.UpdateRole
{
    public class UpdateRoleCommand : IRequest<RoleDetailedDto>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Guid> PermissionIds { get; set; } = new List<Guid>();
    }
}
