using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using FluentValidation.Results;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.InstituicaoModalidadeRegra;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("instituicoes-modalidade-regra")]
    public class InstituicaoModalidadeRegraController : ControllerBase
    {
        private readonly IInstituicaoModalidadeRegraService _service;

        public InstituicaoModalidadeRegraController(IInstituicaoModalidadeRegraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ViewModelPaginada<InstituicaoModalidadeRegraViewModel>>> Buscar(
            [FromQuery] ConsultaInstituicaoModalidadeRegraQueryParam queryParams) =>
             await _service.Buscar(queryParams);

        [HttpPost]
        public async Task<ActionResult<InstituicaoModalidadeRegraViewModel>> Criar(
            [FromBody] CreateInstituicaoModalidadeRegraViewModel viewModel) =>
             await _service.Criar(viewModel);

        [HttpPut]
        public async Task<ActionResult<InstituicaoModalidadeRegraViewModel>> Atualizar(
            [FromBody] CreateInstituicaoModalidadeRegraViewModel viewModel) =>
             await _service.Atualizar(viewModel);
    }
}
