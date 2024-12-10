using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreControl.Application.Features.ProductionLinesFeatures;
using StoreControl.Application.Features.ProductionLinesFeatures.Commands.CreateProductionLine;
using StoreControl.Application.Features.ProductionLinesFeatures.Commands.DeleteProductionLine;
using StoreControl.Application.Features.ProductionLinesFeatures.Queries.GetAllProductionLines;
using StoreControl.Application.Features.ProductionLinesFeatures.Queries.GetProductionLineById;

namespace StoreControl.WebAPI.Controllers
{
    [Route("api/production-lines")]
    [ApiController]
    public class ProductionLinesController : ApiController
    {
        public ProductionLinesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductionLineDto>), 200)]
        public async Task<IActionResult> GetAllProductionLines(CancellationToken cancellationToken)
        {
            var model = new GetAllProductionLinesQuery();

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductionLineById(Guid id, CancellationToken cancellationToken)
        {
            var model = new GetProductionLineByIdQuery { Id = id };

            var response = await _mediator.Send(model, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductionLine([FromBody] CreateProductionLineCommand command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetProductionLineById), new { id = response.Id }, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionLine(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductionLineCommand { Id = id };

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
