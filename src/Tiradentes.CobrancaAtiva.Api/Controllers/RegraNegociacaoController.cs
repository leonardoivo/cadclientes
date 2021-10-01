﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
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
        public async Task<ActionResult<IList<RegraNegociacaoViewModel>>> Buscar()
        {
            return Ok(await _service.Buscar());
        }
    }
}
