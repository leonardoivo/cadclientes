using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiradentes.CobrancaAtiva.Api.Extensions;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Semestre;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("semestre")]
    [Authorize]
    [Autorizacao]
    public class SemestreController : ControllerBase
    {
        private readonly ISemestreService _service;

        public SemestreController(ISemestreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<SemestreViewModel>>> Buscar() =>
             Ok(await _service.Buscar());
    }
}
