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

namespace Tiradentes.CobrancaAtiva.Unit.ItensGeracaoTestes
{
    public class ExisteMatriculaItensGeracao
    {
        private CobrancaAtivaDbContext _context;
        private IItensGeracaoService _service;
        private ItensGeracaoModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ItensGeracaoTests")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            IItensGeracaoRepository repository = new ItensGeracaoRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ItensGeracaoService(repository);

            _model = new ItensGeracaoModel
            {
                DataGeracao = DateTime.Now.AddDays(-7),
                Matricula = 1,
                Periodo = 1,
                Parcela = 1,
                DataVencimento = DateTime.Now.AddDays(7),
                Valor = 100,
                Controle = "Controle",
                CnpjEmpresaCobranca = "CnpjEmpresaCobranc",
                SituacaoAluno = "SituacaoAluno",
                Sistema = "Sistema",
                TipoInadimplencia = "TipoInadimplencia",
                DescricaoInadimplencia = "DescricaoInadimplencia",
                PeriodoOutros = "PeriodoOutros",
                PeriodoChequeDevolvido = "PeriodoChequeDevolvido"
            };

        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Existe Matricula Itens Geracao inválido",
                   Description = "Teste Existe Matricula Itens Geracao no Banco, deve retornar false")]
        public void TesteExisteMatriculaItensGeracaoInvalido()
        {
            var result = _service.ExisteMatricula(_model.CnpjEmpresaCobranca, _model.Matricula, _model.Periodo, 1, _model.PeriodoOutros);

            Assert.AreEqual(false, result);
        }
    }
}
