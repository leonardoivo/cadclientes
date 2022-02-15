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

namespace Tiradentes.CobrancaAtiva.Unit.ItensBaixaTipo1Testes
{
    public class InserirBaixa
    {
        private CobrancaAtivaDbContext _context;
        private IItensBaixasTipo1Service _service;
        private ItensBaixaTipo1Model _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ItensBaixaTipo1Tests")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            IItensBaixasTipo1Repository repository = new ItensBaixasTipo1Repository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ItensBaixasTipo1Service(repository);

            _model = new ItensBaixaTipo1Model
            {
                Sequencia = 1,
                Multa = 10,
                Juros = 10,
                DataVencimento = DateTime.Now.AddDays(7),
                SituacaoAluno = "SituacaoAluno",
                Sistema = "Sistema",
                TipoInadimplencia = "TipoInadimplencia",
                Valor = 100
            };

            if(_context.ItensBaixaTipo1.CountAsync().Result == 0)
            {
                _context.ItensBaixaTipo1.Add(_model);
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
        [TestCase(TestName = "Teste Inserir Baixa 1",
                   Description = "Teste Inserir Baixa 1 no Banco, deve retornar 'InvalidOperationException' por tentar usar procedure in memory")]
        public void TesteInserirBaixaValido()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.InserirBaixa(DateTime.Now, 1, 1, _model.Multa, _model.Juros, _model.DataVencimento, _model.Valor, 1, "28.992.700/0001-29", 1, "Sistema", _model.SituacaoAluno, _model.TipoInadimplencia));
        }
    }
}
