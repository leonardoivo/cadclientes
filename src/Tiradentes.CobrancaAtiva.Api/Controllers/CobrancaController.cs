using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("baixa-cobranca")]
    public class CobrancaController : ControllerBase
    {
        private readonly ICobrancaService _cobrancaService;
        private readonly IBaixasCobrancasService _baixasCobrancaService;
        public CobrancaController(ICobrancaService cobrancaService,
                                  IBaixasCobrancasService baixasCobrancaService)
        {
            _cobrancaService = cobrancaService;
            _baixasCobrancaService = baixasCobrancaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] RespostaViewModel resposta)
        {
            return Ok(await _cobrancaService.Criar(resposta));
        }

        [HttpGet("resultado")]
        public async Task<IActionResult> BuscarResultadoCobranca(string dataBaixa)
        {
            return Ok(await _baixasCobrancaService.Buscar(Convert.ToDateTime(dataBaixa)));
        }
    }
}