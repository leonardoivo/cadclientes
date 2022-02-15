using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Api.Controllers;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Semestre;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;

namespace Tiradentes.CobrancaAtiva.Unit.Semestre
{
    public class ConsultarSemestre
    {
        private CobrancaAtivaDbContext _context;
        private ISemestreService _service;
        private SemestreModel _CriarSemestreModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            ISemestreRepository repository = new SemestreRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new SemestreService(repository, mapper);


            _CriarSemestreModel = new SemestreModel()
            {
                Descricao = "AAA",
                Numeral = 10
            };

            _context.Semestre.Add(_CriarSemestreModel);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Consultar Semestre",
                    Description = "Teste consultando rota de busca de semestres")]
        public async Task TesteBuscarTodos()
        {
            var semestre = await _service.Buscar();
            Assert.AreEqual(1, semestre.Count);
        }
    }
}
