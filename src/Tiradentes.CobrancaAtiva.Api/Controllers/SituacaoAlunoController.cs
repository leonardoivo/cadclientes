using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiradentes.CobrancaAtiva.Api.Extensions;
using Tiradentes.CobrancaAtiva.Application.ViewModels.SituacaoAluno;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("situacao-aluno")]
    [Authorize]
    [Autorizacao]
    public class SituacaoAlunoController : ControllerBase
    {
        private readonly ISituacaoAlunoService _service;

        public SituacaoAlunoController(ISituacaoAlunoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<SituacaoAlunoViewModel>>> Buscar() =>
             Ok(await _service.Buscar());
    }
}
