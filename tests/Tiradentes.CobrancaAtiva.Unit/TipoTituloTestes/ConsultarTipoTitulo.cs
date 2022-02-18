using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Services;

namespace Tiradentes.CobrancaAtiva.Unit.TipoTituloTestes
{
    public class ConsultarTipoTitulo
    {
        private CobrancaAtivaDbContext _context;
        private TipoTituloService _service;
        private TipoTituloModel _CriarTipoTituloModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("TipoTituloTests")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            ITipoTituloRepository repository = new TipoTituloRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new TipoTituloService(repository, mapper);


            _CriarTipoTituloModel = new TipoTituloModel()
            {
                TipoTitulo = "AAA",
                CodigoMagister = "M4H"
            };

            var _CriarTipoTituloModel2 = new TipoTituloModel()
            {
                TipoTitulo = "AAA",
                CodigoMagister = "M4H"
            };

          
            _context.TipoTitulo.Add(_CriarTipoTituloModel);
            _context.TipoTitulo.Add(_CriarTipoTituloModel2);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Consultar Tipo Titulo",
                    Description = "Teste consultando rota de busca de Tipo Titulo")]
        public async Task TesteBuscarTodos()
        {
            var SituacaoAluno = await _service.Buscar();
            Assert.AreEqual(2, SituacaoAluno.Count);
        }
    }
}
