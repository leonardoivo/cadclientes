﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("empresa-parceira")]
    public class EmpresaParceiraController : ControllerBase
    {
        private readonly IEmpresaParceiraService _service;

        public EmpresaParceiraController(IEmpresaParceiraService service)
        {
            _service = service;
        }

        [HttpGet("verifica-cnpj-cadastrado")]
        public async Task<ActionResult<bool>> VerificarCnpjJaCadastrado(string Cnpj)
        {
            await _service.VerificarCnpjJaCadastrado(Cnpj);
            return Ok();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EmpresaParceiraViewModel>> Buscar(int Id)
        {
            return await _service.BuscarPorId(Id);
        }

        [HttpGet]
        public async Task<ActionResult<ViewModelPaginada<BuscaEmpresaParceiraViewModel>>> Buscar(
            [FromQuery] ConsultaEmpresaParceiraQueryParam queryParams) =>
             await _service.Buscar(queryParams);



        [HttpPost]
        public async Task<ActionResult<EmpresaParceiraViewModel>> Criar(
            [FromBody] EmpresaParceiraViewModel viewModel) =>
             await _service.Criar(viewModel);

        [HttpPut]
        public async Task<ActionResult<EmpresaParceiraViewModel>> Atualizar([FromBody] EmpresaParceiraViewModel viewModel)
        {
            return await _service.Atualizar(viewModel);
        }

        [HttpDelete]
        public async Task<ActionResult<EmpresaParceiraViewModel>> Deletar([FromBody] EmpresaParceiraViewModel viewModel)
        {
            await _service.Deletar(viewModel);
            return Ok();
        }
    }
}
