using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("curso")]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _service;

        public CursoController(ICursoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<CursoViewModel>>> Buscar() =>
             Ok(await _service.Buscar());

        [HttpGet("por-instituicao-modalidade/{instituicaoId:int}/{modalidadeId:int}")]
        public async Task<ActionResult<IList<CursoViewModel>>> Buscar(int instituicaoId, int modalidadeId) =>
             Ok(await _service.BuscarPorInstituicaoModalidade(instituicaoId, modalidadeId));
    }
}
