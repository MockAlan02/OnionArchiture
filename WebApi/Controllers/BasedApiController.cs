using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public abstract class BasedApiController
    {
        private Mediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
