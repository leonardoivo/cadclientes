using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("instituicao")]
    public class InstituicaoController : ControllerBase
    {
        private readonly IInstituicaoService _service;

        public InstituicaoController(IInstituicaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<InstituicaoViewModel>>> Buscar() =>
             Ok(await _service.Buscar());
    }
}
