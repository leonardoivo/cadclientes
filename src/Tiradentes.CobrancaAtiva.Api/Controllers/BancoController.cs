using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Banco;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("banco")]
    public class BancoController : ControllerBase
    {
        private readonly IBancoService _service;

        public BancoController(IBancoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<BancoViewModel>>> Buscar() =>
             Ok(await _service.Buscar());
    }
}
