using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("regra-negociacao")]
    public class RegraNegociacaoController : ControllerBase
    {
        private readonly IRegraNegociacaoService _service;

        public RegraNegociacaoController(IRegraNegociacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ModelPaginada<BuscaRegraNegociacao>>> Buscar([FromQuery] ConsultaRegraNegociacaoQueryParam queryParam)
        {
            var retoron = await _service.Buscar(queryParam);
            var config = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            
            return Ok(JsonConvert.SerializeObject(retoron, config));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BuscaRegraNegociacaoViewModel>> Buscar(int id)
        {
            return await _service.BuscarPorId(id);
        }

        [HttpPost]
        public async Task<ActionResult<RegraNegociacaoViewModel>> Criar(
            [FromBody] CriarRegraNegociacaoViewModel viewModel)
        {
            var conflito = _service.VerificarRegraConflitante(viewModel);

            if (conflito.Result != null)
            {
                return StatusCode((int)HttpStatusCode.NonAuthoritativeInformation, conflito.Result);
            }
            return await _service.Criar(viewModel);
        }

        [HttpPut]
        public async Task<ActionResult<RegraNegociacaoViewModel>> Alterar(
            [FromBody] AlterarRegraNegociacaoViewModel viewModel)
        {
            var conflito = _service.VerificarRegraConflitante(viewModel);

            if (conflito.Result != null)
            {
                return StatusCode((int)HttpStatusCode.NonAuthoritativeInformation, conflito.Result);
            }

            return await _service.Alterar(viewModel);
        }
    }
}
