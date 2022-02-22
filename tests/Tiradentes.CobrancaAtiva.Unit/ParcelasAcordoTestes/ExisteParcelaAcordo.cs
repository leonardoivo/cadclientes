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

namespace Tiradentes.CobrancaAtiva.Unit.ParcelasAcordoTestes
{
    public class ExisteParcelaAcordo
    {
        private CobrancaAtivaDbContext _context;
        private IParcelasAcordoService _service;
        private ParcelasAcordoModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ParcelasAcordoTestes2")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);

            IdAlunoRepository idAlunoRepository = new IdAlunoRepository(_context);
            ParcelaTituloRepository parcelaTituloRepository = new ParcelaTituloRepository(_context);
            IParcelasAcordoRepository repository = new ParcelasAcordoRepository(idAlunoRepository, parcelaTituloRepository, _context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ParcelasAcordoService(repository);

            _model = new ParcelasAcordoModel
            {
                NumeroAcordo = 1,        
                Parcela = 1,
                DataBaixa = DateTime.Now,
                DataVencimento = DateTime.Now,
                DataPagamento = DateTime.Now,
                DataBaixaPagamento = DateTime.Now,
                Valor = 0,
                ValorPago = 10,
                CnpjEmpresaCobranca = "01.555.824/0001-90",        
                Sistema = "Sistema",
                TipoInadimplencia = "TipoInadimplencia",
                CodigoBanco = 1,
                SituacaoPagamento = "SituacaoPagamento"
            };

            if(_context.ParcelasAcordoModel.CountAsync().Result == 0)
            {
                _context.ParcelasAcordoModel.Add(_model);
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
        [TestCase(TestName = "Teste verificar parcela acordo válido",
                   Description = "Teste verificar parcela acordo no Banco")]
        public void TesteExisteParcelaAcordoValido()
        {
            var result = _service.ExisteParcelaAcordo(_model.Parcela, _model.NumeroAcordo);

            Assert.AreEqual(true, result);
        }
    }
}
