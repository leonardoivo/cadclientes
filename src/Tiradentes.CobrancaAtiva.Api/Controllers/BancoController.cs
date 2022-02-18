using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiradentes.CobrancaAtiva.Api.Extensions;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Banco;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("banco")]
    [Authorize]
    [Autorizacao]
    public class BancoController : ControllerBase
    {
        private readonly IBancoService _service;
        private readonly IBancoMagisterService _magisterService;

        public BancoController(IBancoService service, IBancoMagisterService magisterService)
        {
            _service = service;
            _magisterService = magisterService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<BancoViewModel>>> Buscar() =>
             Ok(await _service.Buscar());

        [HttpGet("listar-magister")]
        public async Task<ActionResult<IList<BancoMagisterViewModel>>> BuscarMagister() =>
             Ok(await _magisterService.Buscar());
    }
}
