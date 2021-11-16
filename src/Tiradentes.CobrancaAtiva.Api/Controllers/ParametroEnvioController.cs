using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("parametro-envio")]
    public class ParametroEnvioController : ControllerBase
    {
        private readonly IParametroEnvioService _service;

        public ParametroEnvioController(IParametroEnvioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<BuscaParametroEnvioViewModel>>> Buscar([FromQuery] ConsultaParametroEnvioQueryParam queryParam)
        {
            return Ok(await _service.Buscar(queryParam));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BuscaParametroEnvioViewModel>> Buscar(int id)
        {
            return await _service.BuscarPorId(id);
        }

        [HttpPost]
        public async Task<ActionResult<ParametroEnvioViewModel>> Criar(
            [FromBody] CriarParametroEnvioViewModel viewModel)
        {
            return await _service.Criar(viewModel);
        }

        [HttpPut]
        public async Task<ActionResult<ParametroEnvioViewModel>> Alterar(
            [FromBody] AlterarParametroEnvioViewModel viewModel)
        {
            return await _service.Alterar(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ParametroEnvioViewModel>> Deletar(int id)
        {
            await _service.Deletar(id);
            return NoContent();
        }
    }
}
