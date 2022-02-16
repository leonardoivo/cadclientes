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

namespace Tiradentes.CobrancaAtiva.Unit.AcordoCobrancaTestes
{
    public class ObterMatricula
    {
        private CobrancaAtivaDbContext _context;
        private IAcordoCobrancaService _service;
        private AcordosCobrancasModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("AcordoCobrancaTestes3")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            IAcordoCobrancasRepository repository = new AcordoCobrancasRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new AcordoCobrancaService(repository);

            _model = new AcordosCobrancasModel
            {
                CnpjEmpresaCobranca = "01.555.824/0001-90",
                NumeroAcordo = 10,
                DataBaixa = DateTime.Now,
                Data = DateTime.Now,
                TotalParcelas = 2,
                ValorTotal = 100,
                Mora = 100,
                Multa = 100,
                SaldoDevedor = 100,
                Matricula = 1,
                CPF = "01234565478",
                Sistema = "Sistema",
                TipoInadimplencia = "Inadimplencia",
            };

            if(_context.AcordosCobrancasModel.CountAsync().Result == 0)
            {
                _context.AcordosCobrancasModel.Add(_model);
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
        [TestCase(TestName = "Teste obter matrícula acordo de cobrança válido",
                   Description = "Teste obter matrícula acordo de cobrança no Banco")]
        public void TesteObterMatriculaValido()
        {
            var result = _service.ObterMatricula(_model.NumeroAcordo);

            Assert.AreEqual(10, result);
        }
    }
}