using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("baixa-cobranca")]
    public class CobrancaController : ControllerBase
    {
        private readonly ICobrancaService _cobrancaService;
        public CobrancaController(ICobrancaService cobrancaService)
        {
            _cobrancaService = cobrancaService;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] RespostaViewModel resposta)
        {
            return Ok(await _cobrancaService.Criar(resposta));
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] RespostaViewModel resposta)
        {
            return Ok(await _cobrancaService.Listar(resposta));
        }

        [HttpGet("listar-filtros-matricula")]
        public async Task<IActionResult> ListarFiltrosMatricula()
        {
            return Ok(await _cobrancaService.ListarFiltrosMatricula());
        }

        [HttpGet("listar-filtros-acordo")]
        public async Task<IActionResult> ListarFiltrosAcordo()
        {
            return Ok(await _cobrancaService.ListarFiltrosAcordo());
        }

        [HttpGet("listar-filtros-cpf")]
        public async Task<IActionResult> ListarFiltroCpf()
        {
            return Ok(await _cobrancaService.ListarFiltroCpf());
        }

        [HttpGet("listar-filtros-nome-aluno")]
        public async Task<IActionResult> ListarFiltroNomeAluno()
        {
            return Ok(await _cobrancaService.ListarFiltroNomeAluno());
        }        
    }
}