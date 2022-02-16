using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiradentes.CobrancaAtiva.Api.Extensions;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("tipo-pagamento")]
    [Authorize]
    [Autorizacao]
    public class TipoPagamentoController : ControllerBase
    {
        private readonly ITipoPagamentoService _service;

        public TipoPagamentoController(ITipoPagamentoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<TipoPagamentoViewModel>>> Buscar() =>
             Ok(await _service.Buscar());
    }
}
