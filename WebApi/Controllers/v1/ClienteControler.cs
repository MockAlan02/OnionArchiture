using Application.Features.Clientes.commands.CreateClienteCommand;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClienteControler : BasedApiController
    {
        //POST api/Controller
        [HttpPost]
        public async Task<IActionResult> Post(CreateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
