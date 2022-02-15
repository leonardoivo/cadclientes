using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using System;

namespace Tiradentes.CobrancaAtiva.Unit.ItensBaixaTipo3Testes
{
    public class InserirBaixa
    {
        private CobrancaAtivaDbContext _context;
        private IItensBaixasTipo3Service _service;
        private ItensBaixaTipo3Model _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ItensBaixaTipo3Tests")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            IItensBaixasTipo3Repository repository = new ItensBaixasTipo3Repository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ItensBaixasTipo3Service(repository);

            _model = new ItensBaixaTipo3Model
            {
                Sequencia = 1,
                DataPagamento = DateTime.Now.AddDays(-7),
                SituacaoAluno = "SituacaoAluno",
                Sistema = "Sistema",
                TipoInadimplencia = "TipoInadimplencia",
                ValorPago = 100,
                Tipo_Pagamento = "Tipo_Pagamento",
            };

            if(_context.ItensBaixaTipo3.CountAsync().Result == 0)
            {
                _context.ItensBaixaTipo3.Add(_model);
                _context.SaveChanges();
            }
            
            _context.ChangeTracker.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Matricula Itens Baixa 3",
                   Description = "Teste Atualizar Matricula Itens Baixa 3 no Banco, deve retornar 'InvalidOperationException' por tentar usar procedure in memory")]
        public void TesteInserirBaixaValido()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.InserirBaixa(DateTime.Now, 1, 1, _model.Parcela, _model.DataPagamento, _model.ValorPago, 1, "28.992.700/0001-29", "Sistema", _model.SituacaoAluno, _model.TipoInadimplencia, _model.Tipo_Pagamento));
        }
    }
}
