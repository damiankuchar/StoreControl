using MediatR;
using System.Text.Json.Serialization;

namespace StoreControl.Application.Features.UsersFeatures.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDetailedDto>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public List<Guid> RoleIds { get; set; } = new List<Guid>();
    }
}
