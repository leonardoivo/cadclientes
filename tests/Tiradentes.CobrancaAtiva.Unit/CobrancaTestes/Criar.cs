using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using Moq;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Tiradentes.CobrancaAtiva.Unit.CobrancaTestes
{
    public class Criar
    {
        private CobrancaAtivaDbContext _context;
        private ICobrancaService _service;
        private IMapper _mapper;
        private IServiceScopeFactory _scopeFactory;
        private Mock<ICobrancaRepository> _cobrancaRepository;
        private Mock<IAlunosInadimplentesRepository> _alunosInadimplentesRepository;
        private CacheServiceRepository _cacheServiceRepository;
        private IRegraNegociacaoRepository _regraNegociacaoRepository;
        private IInstituicaoRepository _instituicaoRepository;
        private IIdAlunoRepository _idAlunoRepository;
        private IParcelaTituloRepository _parcelaTituloRepository;
        private IParcelasAcordoRepository _parcelasAcordoRepository;
        private IModalidadeRepository _modalidadeRepository;
        private ICursoRepository _cursoRepository;
        private IAcordoCobrancasRepository _acordoCobrancasRepository;
        private IRegraNegociacaoService _regraNegociacaoService;
        private IInstituicaoService _instituicaoService;
        private IParcelasAcordoService _parcelaService;
        private IModalidadeService _modalidadeService;
        private ICursoService _cursoService;
        private IParcelasAcordoService _parcelasAcordoService;
        private IAcordoCobrancaService _acordoCobrancaService;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaTests2")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);

            _cobrancaRepository = new Mock<ICobrancaRepository>();
            _alunosInadimplentesRepository = new Mock<IAlunosInadimplentesRepository>();
            _cacheServiceRepository = new CacheServiceRepository(_scopeFactory);
            _regraNegociacaoRepository = new RegraNegociacaoRepository(_cacheServiceRepository, _context);
            _instituicaoRepository = new InstituicaoRepository(_context);
            _idAlunoRepository = new IdAlunoRepository(_context);
            _parcelaTituloRepository = new ParcelaTituloRepository(_context);
            _parcelasAcordoRepository = new ParcelasAcordoRepository(_idAlunoRepository, _parcelaTituloRepository, _context);
            _modalidadeRepository = new ModalidadeRepository(_context);
            _cursoRepository = new CursoRepository(_context);
            _acordoCobrancasRepository = new AcordoCobrancasRepository(_context);

            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());

            _regraNegociacaoService = new RegraNegociacaoService(_regraNegociacaoRepository, _mapper);
            _instituicaoService = new InstituicaoService(_instituicaoRepository, _mapper);
            _parcelaService = new ParcelasAcordoService(_parcelasAcordoRepository);
            _modalidadeService = new ModalidadeService(_modalidadeRepository, _mapper);
            _cursoService = new CursoService(_cursoRepository, _mapper);
            _parcelasAcordoService = new ParcelasAcordoService(_parcelasAcordoRepository);
            _acordoCobrancaService = new AcordoCobrancaService(_acordoCobrancasRepository);

            _service = new CobrancaService(
                        _cobrancaRepository.Object,
                        _alunosInadimplentesRepository.Object,
                        _regraNegociacaoService,
                        _instituicaoService,
                        _parcelaService,
                        _modalidadeService,
                        _cursoService,
                        _parcelasAcordoService,
                        _acordoCobrancaService,
                        _mapper
                    );
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste criar cobrança",
                   Description = "Teste criar cobrança")]
        public async Task TesteCriarValido()
        {
            var viewModel = new CriarRespostaViewModel()
            {
                TipoRegistro = 1,
                CnpjEmpresaCobranca = "28992700000129",
                SituacaoAluno = "SituacaoAluno",
                Sistema = "Sistema",
                TipoInadimplencia = "TipoInadimplencia",
                ChaveInadimplencia = "ChaveInadimplencia",
                CodigoInstituicaoEnsino = 1,
                Curso = 1,
                CPF = 34567809811,
                NomeAluno = "Aluno Teste",
                Matricula = 1,
                NumeroAcordo = 1,
                Parcela = 1,
                Periodo = "1",
                IdTitulo = 1,
                IdAluno = 1,
                IdPessoa = 1,
                CodigoAtividade = 1,
                NumeroEvt = 1,
                CodigoBanco = 1,
                CodigoAgencia = 1,
                NumeroConta = 1,
                NumeroCheque = 1,
                JurosParcela = 1,
                MultaParcela = 1,
                ValorTotalParcela = 1,
                DataFechamentoAcordo = DateTime.Now.AddDays(-7),
                TotalParcelasAcordo = 6,
                DataVencimentoParcela = DateTime.Now.AddDays(7),
                ValorParcela = 100,
                SaldoDevedorTotal = 600,
                Produto = "Produto",
                DescricaoProduto = "DescricaoProduto",
                CodigoControleCliente = "CodigoControleCliente",
                NossoNumero = "NossoNumero",
                DataPagamento = null,
                DataBaixa = null,
                ValorPago = 100,
                TipoPagamento = "TipoPagamento"
            };

            var result = await _service.Criar(viewModel, viewModel.CnpjEmpresaCobranca);

            Assert.AreEqual(viewModel.CnpjEmpresaCobranca, result.CnpjEmpresaCobranca);
        }
    }
}
