using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Application.Utils;
using Tiradentes.CobrancaAtiva.Application.QueryParams;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresaParceiraController : ControllerBase
    {
        private readonly IEmpresaParceiraService _service;

        public EmpresaParceiraController(IEmpresaParceiraService service)
        {
            _service = service;
        }

        [HttpGet("VerificaCnpjCadastrado")]
        public async Task<ActionResult<bool>> VerificarCnpjJaCadastrado(string Cnpj)
        {
            await _service.VerificarCnpjJaCadastrado(Cnpj);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IList<BuscaEmpresaParceiraViewModel>>> Buscar([FromQuery] ConsultaEmpresaParceiraQueryParam queryParams)
        {
            return Ok(await _service.Buscar());
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaParceiraViewModel>> Criar([FromBody] EmpresaParceiraViewModel viewModel)
        {
            return await _service.Criar(viewModel);
        }
    }
}
