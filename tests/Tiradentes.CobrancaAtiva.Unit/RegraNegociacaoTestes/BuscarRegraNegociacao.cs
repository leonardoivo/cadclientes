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
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Tiradentes.CobrancaAtiva.Unit.RegraNegociacaoTestes
{
    public class BuscarRegraNegociacao
    {
        private CobrancaAtivaDbContext _context;
        private IServiceScopeFactory _serviceScopeFactory;
        private CacheServiceRepository _cacheServiceRepository;
        private IRegraNegociacaoService _service;
        private RegraNegociacaoModel _model;
        private IMapper _mapper;
        private CriarRegraNegociacaoViewModel _criarViewModel;
        private CursoModel _CriarCursoModel;
        private TituloAvulsoModel _CriarTituloAvulsoModel;
        private SituacaoAlunoModel _CriarSituacaoAlunoModel;
        private TipoTituloModel _CriarTipoTituloModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("RegraNegociacaoTests3")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);

            var services = new ServiceCollection();

            services.AddScoped<ICursoRepository, CursoRepository>();
            services.AddScoped<ICursoService, CursoService>();

            services.AddScoped<ITituloAvulsoRepository, TituloAvulsoRepository>();
            services.AddScoped<ITituloAvulsoService, TituloAvulsoService>();

            services.AddScoped<ISituacaoAlunoRepository, SituacaoAlunoRepository>();
            services.AddScoped<ISituacaoAlunoService, SituacaoAlunoService>();

            services.AddScoped<ITipoTituloRepository, TipoTituloRepository>();
            services.AddScoped<ITipoTituloService, TipoTituloService>();

            services.AddScoped<MongoContext>();
            services.AddDbContext<CobrancaAtivaDbContext>(options =>
                options.UseInMemoryDatabase("CobrancaAtivaTests3")); 
            services.AddDbContext<CobrancaAtivaScfDbContext>(options =>
                options.UseInMemoryDatabase("CobrancaAtivaTests3"));

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService(typeof(CobrancaAtivaDbContext));
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider);
            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);

            _cacheServiceRepository = new CacheServiceRepository(serviceScopeFactory.Object);
            IRegraNegociacaoRepository repository = new RegraNegociacaoRepository(_cacheServiceRepository, _context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new RegraNegociacaoService(repository, _mapper);

            _CriarCursoModel = new CursoModel()
            {
                Descricao = "teste",
                ModalidadeId = 1,
                InstituicaoId = 1,
            };
            
            _CriarTituloAvulsoModel = new TituloAvulsoModel()
            {
                Descricao = "teste"
            };

            _CriarSituacaoAlunoModel = new SituacaoAlunoModel()
            {
                Situacao = "teste"
            };

            _CriarTipoTituloModel = new TipoTituloModel()
            {
                TipoTitulo = "teste"
            };

            var CriarInstituicaoModel = new InstituicaoModel()
            {
                Instituicao = "teste"
            };

            var CriarModalidadeModel = new ModalidadeModel()
            {
                Modalidade = "teste"
            };

            
            _context.Curso.Add(_CriarCursoModel);
            _context.TituloAvulso.Add(_CriarTituloAvulsoModel);
            _context.SituacaoAluno.Add(_CriarSituacaoAlunoModel);
            _context.TipoTitulo.Add(_CriarTipoTituloModel);
            _context.Instituicao.Add(CriarInstituicaoModel);
            _context.Modalidade.Add(CriarModalidadeModel);
           
            _context.SaveChanges();

            _criarViewModel = new CriarRegraNegociacaoViewModel
            {
                InstituicaoId = CriarInstituicaoModel.Id,
                ModalidadeId = CriarModalidadeModel.Id,
                PercentJurosMultaAVista = 0,
                PercentValorAVista = 0,
                PercentJurosMultaCartao  = 0,
                PercentValorCartao = 0,
                QuantidadeParcelasCartao = 0,
                PercentJurosMultaBoleto = 0,
                PercentValorBoleto = 0,
                PercentEntradaBoleto = 0,
                QuantidadeParcelasBoleto = 0,
                Status = true,
                InadimplenciaInicial = DateTime.Now,
                InadimplenciaFinal = DateTime.Now,
                ValidadeInicial = DateTime.Now,
                ValidadeFinal = DateTime.Now,
                CursoIds = new int[1]{ _CriarCursoModel.Id },
                SituacaoAlunoIds = new int[1]{ _CriarSituacaoAlunoModel.Id },
                TitulosAvulsosId = new int[1]{ _CriarTituloAvulsoModel.Id },
                TipoTituloIds = new int[1]{ _CriarTipoTituloModel.Id }
            };

            if(_context.RegraNegociacao.CountAsync().Result == 0)
            {
                _model = _mapper.Map<RegraNegociacaoModel>(_criarViewModel);
                _context.RegraNegociacao.Add(_model);
                _context.SaveChanges();

                _criarViewModel.Status = false;

                _context.RegraNegociacao.Add(_mapper.Map<RegraNegociacaoModel>(_criarViewModel));
                _context.SaveChanges();
            }
            
            _context.ChangeTracker.Clear();
        }

        [Test]
        [TestCase(TestName = "Teste Buscar Regra Negociacao válido",
                   Description = "Teste Buscar Regra Negociacao no Banco")]
        public async Task TesteBuscarRegraNegociacaoValido()
        {
            var queryParam = new ConsultaRegraNegociacaoQueryParam()
            {
                Status = true,
            };

            var Regras = await _service.Buscar(queryParam);

            Assert.AreEqual(1, Regras.TotalItems);
        }

        [Test]
        [TestCase(TestName = "Teste Buscar Regra Negociacao inválido",
                   Description = "Teste Buscar Regra Negociacao no Banco")]
        public async Task TesteBuscarRegraNegociacaoInvalido()
        {
            var queryParam = new ConsultaRegraNegociacaoQueryParam()
            {
                InstituicaoId = 123123,
                ModalidadeId = 112311,
                Status = true,
                ValidadeInicial = DateTime.Now.AddDays(-7),
                ValidadeFinal = DateTime.Now.AddDays(7),
                InadimplenciaInicial = DateTime.Now.AddDays(-7),
                InadimplenciaFinal = DateTime.Now.AddDays(7),
                Cursos = new int[1]{ _CriarCursoModel.Id },
                SituacoesAlunos = new int[1]{ _CriarSituacaoAlunoModel.Id },
                TitulosAvulsos = new int[1]{ _CriarTituloAvulsoModel.Id },
                TiposTitulos = new int[1]{ _CriarTipoTituloModel.Id }
            };

            var Regras = await _service.Buscar(queryParam);

            Assert.AreEqual(0, Regras.TotalItems);
        }
    }
}
