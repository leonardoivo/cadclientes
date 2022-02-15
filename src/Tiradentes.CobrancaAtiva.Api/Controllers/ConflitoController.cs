﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiradentes.CobrancaAtiva.Api.Extensions;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;


namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("conflito")]
    [Authorize]
    [Autorizacao]
    public class ConflitoController : ControllerBase
    {
        private readonly IConflitoService _service;

        public ConflitoController(IConflitoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<BuscaConflitoViewModel>>> Buscar([FromQuery] ConsultaConflitoQueryParam queryParam)
        {
            return Ok(await _service.Buscar(queryParam));
        }
    }
}
