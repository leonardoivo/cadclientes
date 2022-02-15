﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiradentes.CobrancaAtiva.Api.Extensions;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Endereco;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("endereco")]
    [Authorize]
    [Autorizacao]
    public class EnderecoController : Controller
    {
        private readonly IEnderecoService _service;

        public EnderecoController(IEnderecoService service)
        {
            _service = service;
        }

        [HttpGet("busca-por-cep/{cep}")]
        public async Task<ActionResult<EnderecoViewModel>> BuscarPorCep(string cep)
        {
            return await _service.BuscarPorCep(cep);
        }
    }
}
