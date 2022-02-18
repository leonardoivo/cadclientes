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

namespace Tiradentes.CobrancaAtiva.Unit.TipoPagamentoTestes
{
    public class ConsultarTipoPagamento
    {
        private CobrancaAtivaDbContext _context;
        private TipoPagamentoService _service;
        private TipoPagamentoModel _CriarTipoPagamentoModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("TipoPagamentoTests")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            ITipoPagamentoRepository repository = new TipoPagamentoRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new TipoPagamentoService(repository, mapper);


            _CriarTipoPagamentoModel = new TipoPagamentoModel()
            {
                TipoPagamento = "AAA",
            };

            var _CriarTipoPagamentoModel2 = new TipoPagamentoModel()
            {
                TipoPagamento = "AAA",
            };

            var _CriarTipoPagamentoModel3 = new TipoPagamentoModel()
            {
                TipoPagamento = "AAA",
            };

            _context.TipoPagamento.Add(_CriarTipoPagamentoModel);
            _context.TipoPagamento.Add(_CriarTipoPagamentoModel2);
            _context.TipoPagamento.Add(_CriarTipoPagamentoModel3);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Consultar Tipo Pagamento",
                    Description = "Teste consultando rota de busca de Tipo Pagamento")]
        public async Task TesteBuscarTodos()
        {
            var SituacaoAluno = await _service.Buscar();
            Assert.AreEqual(3, SituacaoAluno.Count);
        }
    }
}
