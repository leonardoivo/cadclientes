using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoService _service;

        public EnderecoController(IEnderecoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPorCep()
        {
            await _service.BuscarPorCep();
            return Ok();
        }
    }
}
