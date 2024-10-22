using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreControl.Application.Features.RoleFeatures;
using StoreControl.Application.Features.RoleFeatures.Commands.CreateRole;
using StoreControl.Application.Features.RoleFeatures.Commands.DeleteRole;
using StoreControl.Application.Features.RoleFeatures.Commands.UpdateRole;
using StoreControl.Application.Features.RoleFeatures.Queries.GetAllRoles;
using StoreControl.Application.Features.RoleFeatures.Queries.GetRoleById;

namespace StoreControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiController
    {
        public RoleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleDto>), 200)]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var model = new GetAllRolesQuery();

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RoleDetailedDto), 200)]
        public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellationToken)
        {
            var model = new GetRoleByIdQuery { Id = id };

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoleDetailedDto), 201)]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetRoleById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RoleDetailedDto), 200)]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var response = await _mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteRoleCommand { Id = id };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
