using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StoreControl.WebAPI.Controllers
{
    public class ApiController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public ApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
