using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito;
using Tiradentes.CobrancaAtiva.Services.Interfaces;


namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("Conflito")]
    public class ConflitoController : ControllerBase
    {
        private readonly IConflitoService _service;

        public ConflitoController(IConflitoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<ConflitoViewModel>>> Buscar()
        {
            return Ok(await _service.Buscar());
        }

        [HttpGet]
        public async Task<ActionResult<IList<ConflitoViewModel>>> Buscar([FromQuery] ConflitoQueryParam queryParam)
        {
            return Ok(await _service.Buscar(queryParam));
        }
    }
}
