﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreControl.Application.Features.AuthFeatures;
using StoreControl.Application.Features.AuthFeatures.Commands.Login;
using StoreControl.Application.Features.AuthFeatures.Commands.Refresh;
using StoreControl.Application.Features.AuthFeatures.Commands.Register;

namespace StoreControl.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(response);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        public async Task<IActionResult> Refresh([FromBody] RefreshCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(response);
        }
    }
}
