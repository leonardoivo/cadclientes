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

namespace Tiradentes.CobrancaAtiva.Unit.ItensBaixaTipo2Testes
{
    public class InserirBaixa
    {
        private CobrancaAtivaDbContext _context;
        private IItensBaixasTipo2Service _service;
        private ItensBaixaTipo2Model _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ItensBaixaTipo2Tests")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            IItensBaixasTipo2Repository repository = new ItensBaixasTipo2Repository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ItensBaixasTipo2Service(repository);

            _model = new ItensBaixaTipo2Model
            {
                Sequencia = 1,
                Periodo = 1,
                DataVencimento = DateTime.Now.AddDays(7),
                SituacaoAluno = "SituacaoAluno",
                PeriodoOutros = "PeriodoOutros",
                Sistema = "Sistema",
                TipoInadimplencia = "TipoInadimplencia",
                Valor = 100
            };

            if(_context.ItensBaixaTipo2.CountAsync().Result == 0)
            {
                _context.ItensBaixaTipo2.Add(_model);
                _context.SaveChanges();
            }
            
            _context.ChangeTracker.Clear();
        }

        [Test]
        [TestCase(TestName = "Teste Teste Inserir Baixa 2",
                   Description = "Teste Teste Inserir Baixa 2 no Banco, deve retornar 'InvalidOperationException' por tentar usar procedure in memory")]
        public void TesteInserirBaixaValido()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.InserirBaixa(DateTime.Now, 1, 1, _model.Parcela, _model.Periodo, _model.DataVencimento, _model.Valor, 1, "28.992.700/0001-29", "Sistema", _model.SituacaoAluno, _model.TipoInadimplencia, _model.PeriodoOutros));
        }
    }
}
