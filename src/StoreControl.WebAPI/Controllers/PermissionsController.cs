using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreControl.Application.Features.PermissionsFeatures;
using StoreControl.Application.Features.PermissionsFeatures.Commands.CreatePermission;
using StoreControl.Application.Features.PermissionsFeatures.Commands.DeletePermission;
using StoreControl.Application.Features.PermissionsFeatures.Commands.UpdatePermission;
using StoreControl.Application.Features.PermissionsFeatures.Queries.GetAllPermisions;
using StoreControl.Application.Features.PermissionsFeatures.Queries.GetPermissionById;

namespace StoreControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ApiController
    {
        public PermissionsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PermissionDto>), 200)]
        public async Task<IActionResult> GetAllPermisions(CancellationToken cancellationToken)
        {
            var model = new GetAllPermisionsQuery();

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PermissionDto), 200)]
        public async Task<IActionResult> GetPermissionById(Guid id, CancellationToken cancellationToken)
        {
            var model = new GetPermissionByIdQuery { Id = id };

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PermissionDto), 201)]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetPermissionById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PermissionDto), 200)]
        public async Task<IActionResult> UpdatePermission(Guid id, [FromBody] UpdatePermissionCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var response = await _mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeletePermission(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeletePermissionCommand { Id = id };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
