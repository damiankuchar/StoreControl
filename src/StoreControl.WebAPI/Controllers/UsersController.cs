using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreControl.Application.Features.UsersFeatures;
using StoreControl.Application.Features.UsersFeatures.Commands.CreateUser;
using StoreControl.Application.Features.UsersFeatures.Commands.DeleteUser;
using StoreControl.Application.Features.UsersFeatures.Queries.GetAllUsers;
using StoreControl.Application.Features.UsersFeatures.Queries.GetUserById;

namespace StoreControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var model = new GetAllUsersQuery();

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDetailedDto), 200)]
        public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var model = new GetUserByIdQuery { Id = id };

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserDetailedDto), 201)]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetUserById), new { id = response.Id }, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand { Id = id };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
