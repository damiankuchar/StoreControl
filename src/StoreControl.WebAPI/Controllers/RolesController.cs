using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreControl.Application.Features.RolesFeatures;
using StoreControl.Application.Features.RolesFeatures.Commands.CreateRole;
using StoreControl.Application.Features.RolesFeatures.Commands.DeleteRole;
using StoreControl.Application.Features.RolesFeatures.Commands.UpdateRole;
using StoreControl.Application.Features.RolesFeatures.Queries.GetAllRoles;
using StoreControl.Application.Features.RolesFeatures.Queries.GetRoleById;
using StoreControl.Infrastructure.Authentication;

namespace StoreControl.WebAPI.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ApiController
    {
        public RolesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [HasPermission("Read_All_Roles")]
        [ProducesResponseType(typeof(IEnumerable<RoleDto>), 200)]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var model = new GetAllRolesQuery();

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [HasPermission("Read_Role")]
        [ProducesResponseType(typeof(RoleDetailedDto), 200)]
        public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellationToken)
        {
            var model = new GetRoleByIdQuery { Id = id };

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        [HasPermission("Create_Role")]
        [ProducesResponseType(typeof(RoleDetailedDto), 201)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetRoleById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [HasPermission("Update_Role")]
        [ProducesResponseType(typeof(RoleDetailedDto), 200)]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var response = await _mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [HasPermission("Delete_Role")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand { Id = id };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
