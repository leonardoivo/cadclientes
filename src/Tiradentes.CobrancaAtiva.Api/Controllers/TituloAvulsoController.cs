using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiradentes.CobrancaAtiva.Api.Extensions;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.TituloAvulso;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("titulo-avulso")]
    [Authorize]
    [Autorizacao]
    public class TituloAvulsoController : ControllerBase
    {
        private readonly ITituloAvulsoService _service;

        public TituloAvulsoController(ITituloAvulsoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TituloAvulsoViewModel>>> Buscar() =>
             Ok(await _service.Buscar());
    }
}
