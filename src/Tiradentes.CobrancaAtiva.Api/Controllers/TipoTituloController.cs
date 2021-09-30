using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("tipo-titulo")]
    public class TipoTituloController : ControllerBase
    {
        private readonly ITipoTituloService _service;

        public TipoTituloController(ITipoTituloService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TipoTituloViewModel>>> Buscar() =>
             Ok(await _service.Buscar());
    }
}
