using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("empresa-parceira")]
    public class EmpresaParceiraController : ControllerBase
    {
        private readonly IEmpresaParceiraService _service;

        public EmpresaParceiraController(IEmpresaParceiraService service)
        {
            _service = service;
        }

        [HttpGet("verifica-cnpj-cadastrado/{cnpj}")]
        public async Task<ActionResult> VerificarCnpjJaCadastrado(string cnpj, int? id = null)
        {
            await _service.VerificarCnpjJaCadastrado(cnpj, id);
            return Ok();
        }

        [HttpGet("enviar-arquivo-cobranca/{Id}")]
        public async Task<ActionResult<EmpresaParceiraViewModel>> EnviarArquivoCobranca(int Id)
        {
            await _service.EnviarArquivoSftp(Id);
            return Ok();
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<EmpresaParceiraViewModel>> Buscar(int Id)
        {
            return await _service.BuscarPorId(Id);
        }

        [HttpGet]
        public async Task<ActionResult<ViewModelPaginada<BuscaEmpresaParceiraViewModel>>> Buscar(
            [FromQuery] ConsultaEmpresaParceiraQueryParam queryParams) =>
             await _service.Buscar(queryParams);



        [HttpPost]
        public async Task<ActionResult<EmpresaParceiraViewModel>> Criar(
            [FromBody] EmpresaParceiraViewModel viewModel) =>
             await _service.Criar(viewModel);

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<ValidationFailure>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(teste))]
        public async Task<ActionResult<EmpresaParceiraViewModel>> Atualizar([FromBody] EmpresaParceiraViewModel viewModel)
        {
            return await _service.Atualizar(viewModel);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<EmpresaParceiraViewModel>> Deletar(int id)
        {
            await _service.Deletar(id);
            return NoContent();
        }
    }

    public class teste {

        public string erro { get; set; }
    }

}
