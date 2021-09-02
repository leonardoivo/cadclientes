using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

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

        [HttpGet]
        public async Task<ActionResult<IList<BuscaEmpresaParceiraViewModel>>> Buscar()
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
