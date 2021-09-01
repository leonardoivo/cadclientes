using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Entities.Dto.Aluno;
using Tiradentes.CobrancaAtiva.Services.Interface.Interfaces;

namespace Tiradentes.CobrancaAtiva.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        private IAlunoService _service;
        private readonly ILogger<TesteController> _logger;

        public TesteController(
            ILogger<TesteController> logger,
            IAlunoService service
        )
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public void Save(AlunoDto dto) => _service.Save(dto);

        [HttpGet]
        public IEnumerable<AlunoDto> List() => _service.List();
    }
}
