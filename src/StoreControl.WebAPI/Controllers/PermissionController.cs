using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreControl.Application.Features.PermissionFeatures.Commands.CreatePermission;
using StoreControl.Application.Features.PermissionFeatures.Commands.DeletePermission;
using StoreControl.Application.Features.PermissionFeatures.Commands.UpdatePermission;
using StoreControl.Application.Features.PermissionFeatures.Queries.GetAllPermisions;
using StoreControl.Application.Features.PermissionFeatures.Queries.GetPermissionById;

namespace StoreControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ApiController
    {
        public PermissionController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermisions(CancellationToken cancellationToken)
        {
            var model = new GetAllPermisionsQuery();

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(Guid id, CancellationToken cancellationToken)
        {
            var model = new GetPermissionByIdQuery { Id = id };

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetPermissionById), new { id = response }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(Guid id, [FromBody] UpdatePermissionCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeletePermissionCommand { Id = id };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
