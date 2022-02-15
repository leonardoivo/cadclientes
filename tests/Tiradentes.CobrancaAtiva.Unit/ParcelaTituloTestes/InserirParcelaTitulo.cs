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

namespace Tiradentes.CobrancaAtiva.Unit.ParcelasTituloTestes
{
    public class InserirParcelaTitulo
    {
        private CobrancaAtivaDbContext _context;
        private IParcelaTituloService _service;
        private ParcelasTitulosModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ParcelasTituloTestes")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);

            IParcelaTituloRepository repository = new ParcelaTituloRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ParcelaTituloService(repository);

            _model = new ParcelasTitulosModel
            {
                CnpjEmpresaCobranca = "CnpjEmpresaCobranca",
                NumeroAcordo = 1,
                Matricula = 1,
                Periodo = 1,        
                PeriodoOutros = "PeriodoOutros",        
                Parcela = 1,        
                Sistema = "Sistema",        
                TipoInadimplencia = "TipoInadimplencia",
                DataBaixa = DateTime.Now,
                DataEnvio = DateTime.Now,
                DataVencimento = DateTime.Now,
                Valor = 100
            };

            if(_context.ParcelasTitulosModel.CountAsync().Result == 0)
            {
                _context.ParcelasTitulosModel.Add(_model);
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
        [TestCase(TestName = "Teste inserir pagamento parcela titulo",
                   Description = "Teste inserir pagamento parcela titulo no banco, deve retornar 'InvalidOperationException' por tentar usar procedure in memory")]
        public void TesteInserirParcelaValido()
        {
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.InserirParcela(_model.NumeroAcordo, _model.Matricula, _model.Periodo, Decimal.ToInt32(_model.Parcela), _model.DataBaixa, _model.DataEnvio, _model.DataVencimento, _model.Valor, _model.CnpjEmpresaCobranca, _model.Sistema, _model.TipoInadimplencia, _model.PeriodoOutros));
        }
    }
}
