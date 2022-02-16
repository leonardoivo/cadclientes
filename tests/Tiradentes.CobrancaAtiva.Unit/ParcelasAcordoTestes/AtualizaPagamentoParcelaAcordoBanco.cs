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
using Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento;

namespace Tiradentes.CobrancaAtiva.Unit.ParcelasAcordoTestes
{
    public class AtualizaPagamentoParcelaAcordoBanco
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
                    .UseInMemoryDatabase("ParcelasAcordoTestes5")
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
                Valor = 100,
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
        [TestCase(TestName = "Teste atualizar pagamento parcela acordoBanco válido",
                   Description = "Teste atualizar pagamento parcela acordoBanco no Banco, deve retornar 'InvalidOperationException' por tentar usar procedure in memory")]
        public void TesteAtualizaPagamentoParcelaAcordoBancoValido()
        {
            var viewModel = new BaixaPagamentoParcelaManualViewModel()
            {
                Parcela = _model.Parcela,
                NumeroAcordo = _model.NumeroAcordo,
                ValorBaixa = _model.Valor,
                DataBaixa = DateTime.Now,
                Matricula = 1,
                CodigoBanco = 1 ,
                Motivo = "teste"
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.AtualizaPagamentoParcelaAcordoBanco(viewModel));
        }
    }
}
