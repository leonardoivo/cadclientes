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

namespace Tiradentes.CobrancaAtiva.Unit.TituloAvulso
{
    public class ConsultarTituloAvulso
    {
        private CobrancaAtivaDbContext _context;
        private TituloAvulsoService _service;
        private TituloAvulsoModel _CriarTituloAvulsoModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("TituloAvulsoTests")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            ITituloAvulsoRepository repository = new TituloAvulsoRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new TituloAvulsoService(repository, mapper);


            _CriarTituloAvulsoModel = new TituloAvulsoModel()
            {
                CodigoGT = 1,
                Descricao = "M4H"
            };

            var _CriarTituloAvulsoModel2 = new TituloAvulsoModel()
            {
                CodigoGT = 2,
                Descricao = "M4H"
            };

            var _CriarTituloAvulsoModel3 = new TituloAvulsoModel()
            {
                CodigoGT = 3,
                Descricao = "M4H"
            };

            var _CriarTituloAvulsoModel4 = new TituloAvulsoModel()
            {
                CodigoGT = 4,
                Descricao = "M4H"
            };

           

          
            _context.TituloAvulso.Add(_CriarTituloAvulsoModel);
            _context.TituloAvulso.Add(_CriarTituloAvulsoModel2);
            _context.TituloAvulso.Add(_CriarTituloAvulsoModel3);
            _context.TituloAvulso.Add(_CriarTituloAvulsoModel4);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Consultar Titulo Avulso",
                    Description = "Teste consultando rota de busca de Titulo Avulso")]
        public async Task TesteBuscarTodos()
        {
            var SituacaoAluno = await _service.Buscar();
            Assert.AreEqual(4, SituacaoAluno.Count);
        }
    }
}
