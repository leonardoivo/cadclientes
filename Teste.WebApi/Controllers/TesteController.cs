using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.Aplicacao.Servicos;
using Teste.Aplicacao.Servicos.Interfaces;
using Teste.Dominio.Entidades;

namespace Teste.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {
        private ITesteServico _service;
        private readonly ILogger<TesteController> _logger;

        public TesteController(ILogger<TesteController> logger)
        {
            _service = new TesteServico();
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Teste.Dominio.Entidades.Teste> Get()
        {
            return _service.List();
        }

        [HttpPost]
        public void Save(Dominio.Entidades.Teste Teste)
        {
            _service.Add(Teste);
        }
    }
}
