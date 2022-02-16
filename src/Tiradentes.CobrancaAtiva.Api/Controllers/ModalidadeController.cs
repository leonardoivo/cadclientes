using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiradentes.CobrancaAtiva.Api.Extensions;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("modalidade")]
    [Authorize]
    [Autorizacao]
    public class ModalidadeController : ControllerBase
    {
        private readonly IModalidadeService _service;

        public ModalidadeController(IModalidadeService service)
        {
            _service = service;
        }

        [HttpGet("por-instituicao/{instituicaoId:int}")]
        public async Task<ActionResult<IList<ModalidadeViewModel>>> Buscar(int instituicaoId) =>
             Ok(await _service.BuscarPorInstituicao(instituicaoId));
    }
}
