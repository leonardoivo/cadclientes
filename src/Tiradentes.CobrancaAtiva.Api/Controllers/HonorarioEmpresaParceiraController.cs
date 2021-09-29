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

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("honorarios")]
    public class HonorarioEmpresaParceiraController : ControllerBase
    {
        private readonly IHonorarioEmpresaParceiraService _service;

        public HonorarioEmpresaParceiraController(IHonorarioEmpresaParceiraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ViewModelPaginada<HonorarioEmpresaParceiraViewModel>>> Buscar(
            [FromQuery] ConsultaHonorarioEmpresaParceiraQueryParam queryParams) =>
             await _service.Buscar(queryParams);

        [HttpPost]
        public async Task<ActionResult<HonorarioEmpresaParceiraViewModel>> Criar(
            [FromBody] CreateHonorarioEmpresaParceiraViewModel viewModel) =>
             await _service.Criar(viewModel);

        [HttpPut]
        public async Task<ActionResult<HonorarioEmpresaParceiraViewModel>> Atualizar(
            [FromBody] CreateHonorarioEmpresaParceiraViewModel viewModel) =>
             await _service.Atualizar(viewModel);
    }
}
