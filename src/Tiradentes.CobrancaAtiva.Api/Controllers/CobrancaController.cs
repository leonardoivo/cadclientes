using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento;
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

        /// <summary>
        /// JSON de exemplo para os respectivos tipos de registros (1,2 e 3)
        /// </summary>
        /// <returns></returns>
        [HttpGet("exemplo-envio-resposta")]
        public IEnumerable<CriarRespostaViewModel> ExemplosRespostas()
        {
            return _cobrancaService.ExemplosRespostas();
        }
        /// <summary>
        /// Faz o envio das respostas de acordos de cobrança (Tipo 1, Tipo 2 e Tipo 3).
        /// </summary>
        /// <param name="resposta"></param>
        /// <returns></returns>
        [HttpPost("enviar-resposta-acordo-cobranca")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] CriarRespostaViewModel resposta)
        {
            return Ok(await _cobrancaService.Criar(resposta));
        }

        [HttpPost("regularizar-acordo-cobranca")]
        public async Task<IActionResult> RegularizarAcordoCobranca([FromBody] RegularizarParcelasAcordoViewModel viewModel)
        {
            return Ok(await _cobrancaService.RegularizarAcordoCobranca(viewModel));
        }

        /// <summary>
        /// Retorna o Historico do processamnento de arquivos.
        /// </summary>
        /// <param name="dataBaixa">dd-mm-yyyy</param>
        /// <returns></returns>
        [HttpGet("resultado/{dataBaixa}")]
        public async Task<IActionResult> BuscarHistoricoProcessamentoCobranca(DateTime dataBaixa)
        {
            return Ok(await _baixasCobrancaService.Buscar(Convert.ToDateTime(dataBaixa)));
        }

        [HttpPost("baixa-manual")]
        public async Task<IActionResult> BaixaManual([FromBody] BaixaPagamentoParcelaManualViewModel baixaPagamento)
        {
            await _cobrancaService.BaixaManual(baixaPagamento);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ConsultaBaixaPagamentoQueryParam resposta)
        {
            return Ok(await _cobrancaService.Listar(resposta));
        }

        [HttpGet("listar-filtros-matricula")]
        public async Task<IActionResult> ListarFiltrosMatricula([FromQuery] string matricula)
        {
            return Ok(await _cobrancaService.ListarFiltrosMatricula(matricula));
        }

        [HttpGet("listar-filtros-acordo")]
        public async Task<IActionResult> ListarFiltrosAcordo([FromQuery] string acordo)
        {
            return Ok(await _cobrancaService.ListarFiltrosAcordo(acordo));
        }

        [HttpGet("listar-filtros-cpf")]
        public async Task<IActionResult> ListarFiltroCpf([FromQuery] string cpf)
        {
            return Ok(await _cobrancaService.ListarFiltroCpf(cpf));
        }

        [HttpGet("listar-filtros-nome-aluno")]
        public async Task<IActionResult> ListarFiltroNomeAluno([FromQuery] string nomeAluno)
        {
            return Ok(await _cobrancaService.ListarFiltroNomeAluno(nomeAluno));
        }
    }
}