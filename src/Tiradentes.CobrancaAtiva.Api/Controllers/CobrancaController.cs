﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiradentes.CobrancaAtiva.Api.Extensions;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        [AutenticacaoEmpresa]
        [HttpPost("enviar-resposta-acordo-cobranca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] CriarRespostaViewModel resposta)
        {
            var cnpj = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "CNPJ");
            return Ok(await _cobrancaService.Criar(resposta, cnpj.Value));
        }

        [Autorizacao]
        [HttpPost("regularizar-acordo-cobranca")]
        public async Task<IActionResult> RegularizarAcordoCobranca(
            [FromBody] RegularizarParcelasAcordoViewModel viewModel)
        {
            return Ok(await _cobrancaService.RegularizarAcordoCobranca(viewModel));
        }

        /// <summary>
        /// Retorna o Historico do processamnento de arquivos.
        /// </summary>
        /// <param name="dataBaixa">dd-mm-yyyy</param>
        /// <returns></returns>
        [Autorizacao]
        [HttpGet("resultado/{dataBaixa}")]
        public async Task<IActionResult> BuscarHistoricoProcessamentoCobranca()
        {
            return Ok(await _baixasCobrancaService.Buscar(new DateTime(2022, 01, 24)));
        }

        [Autorizacao]
        [HttpPost("baixa-manual")]
        public async Task<IActionResult> BaixaManual([FromBody] BaixaPagamentoParcelaManualViewModel baixaPagamento)
        {
            await _cobrancaService.BaixaManual(baixaPagamento);
            return Ok();
        }

        [Autorizacao]
        [HttpGet("baixas")]
        public async Task<ActionResult<ViewModelPaginada<ConsultaBaixaPagamentoViewModel>>> Buscar(
            [FromQuery] ConsultaBaixaCobrancaQueryParam queryParam)
        {
            return await _baixasCobrancaService.Buscar(queryParam);
        }

        [Autorizacao("financeiro_unidadeapenas")]
        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ConsultaBaixaPagamentoQueryParam resposta)
        {
            return Ok(await _cobrancaService.Listar(resposta));
        }

        [Autorizacao("financeiro_unidadeapenas")]
        [HttpGet("listar-filtros-matricula")]
        public async Task<IActionResult> ListarFiltrosMatricula([FromQuery] string matricula)
        {
            return Ok(await _cobrancaService.ListarFiltrosMatricula(matricula));
        }

        [Autorizacao("financeiro_unidadeapenas")]
        [HttpGet("listar-filtros-acordo")]
        public async Task<IActionResult> ListarFiltrosAcordo([FromQuery] string acordo)
        {
            return Ok(await _cobrancaService.ListarFiltrosAcordo(acordo));
        }

        [Autorizacao("financeiro_unidadeapenas")]
        [HttpGet("listar-filtros-cpf")]
        public async Task<IActionResult> ListarFiltroCpf([FromQuery] string cpf)
        {
            return Ok(await _cobrancaService.ListarFiltroCpf(cpf));
        }

        [Autorizacao("financeiro_unidadeapenas")]
        [HttpGet("listar-filtros-nome-aluno")]
        public async Task<IActionResult> ListarFiltroNomeAluno([FromQuery] string nomeAluno)
        {
            return Ok(await _cobrancaService.ListarFiltroNomeAluno(nomeAluno));
        }
    }
}