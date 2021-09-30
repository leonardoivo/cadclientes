﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("situacao")]
    public class SituacaoController : ControllerBase
    {
        private readonly ISituacaoService _service;

        public SituacaoController(ISituacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IList<SituacaoViewModel>>> Buscar() =>
             Ok(await _service.Buscar());
    }
}
