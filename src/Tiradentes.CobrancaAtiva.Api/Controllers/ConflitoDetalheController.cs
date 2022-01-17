﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using Tiradentes.CobrancaAtiva.Services.Interfaces;


namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("conflito-detalhe")]
    public class ConflitoDetalheController : ControllerBase
    {
        private readonly IConflitoDetalheService _service;

        public ConflitoDetalheController(IConflitoDetalheService service)
        {
            _service = service;
        }

        [HttpGet("{idConflito:int}")]
        public async Task<ActionResult<IList<ConflitoDetalheViewModel>>> BuscarPorIdConflitoComRelacionamentos(int idConflito)
        {
            return Ok(await _service.BuscarPorIdConflitoComRelacionamentos(idConflito));
        }
    }
}