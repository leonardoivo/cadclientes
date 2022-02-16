using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
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
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Unit.RegraNegociacaoTestes
{
    public class CriarRegraNegociacao
    {
        private CobrancaAtivaDbContext _context;
        private IServiceScopeFactory _scopeFactory;
        private CacheServiceRepository _cacheServiceRepository;
        private IRegraNegociacaoService _service;
        private RegraNegociacaoModel _model;
        private IMapper _mapper;
        private CriarRegraNegociacaoViewModel _criarViewModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests2")
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
                options.UseInMemoryDatabase("CobrancaAtivaTests2")); 
            services.AddDbContext<CobrancaAtivaScfDbContext>(options =>
                options.UseInMemoryDatabase("CobrancaAtivaTests2"));

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

            _criarViewModel = new CriarRegraNegociacaoViewModel
            {
                InstituicaoId = 1,
                ModalidadeId = 1,
                PercentJurosMultaAVista = 1,
                PercentValorAVista = 1,
                PercentJurosMultaCartao  = 1,
                PercentValorCartao = 1,
                QuantidadeParcelasCartao = 1,
                PercentJurosMultaBoleto = 1,
                PercentValorBoleto = 1,
                PercentEntradaBoleto = 1,
                QuantidadeParcelasBoleto = 1,
                Status = true,
                InadimplenciaInicial = DateTime.Now,
                InadimplenciaFinal = DateTime.Now,
                ValidadeInicial = DateTime.Now,
                ValidadeFinal = DateTime.Now,
                CursoIds = new int[1]{ 1 },
                SituacaoAlunoIds = new int[1]{ 1 },
                TitulosAvulsosId = new int[1]{ 1 },
                TipoTituloIds = new int[1]{ 1 },
            };

            if(_context.RegraNegociacao.CountAsync().Result == 0)
            {
                _model = _mapper.Map<RegraNegociacaoModel>(_criarViewModel);
                _context.RegraNegociacao.Add(_model);
                _context.SaveChanges();
            }
        }

        [Test]
        [TestCase(TestName = "Teste Criar Regra Negociacao válido",
                   Description = "Teste Criar Regra Negociacao no Banco")]
        public async Task TesteCriarRegraNegociacaoValido()
        {
            _criarViewModel.ValidadeInicial = DateTime.Now.AddDays(-7);

            await _service.Criar(_criarViewModel);

            var queryParam = new ConsultaRegraNegociacaoQueryParam()
            {
                ValidadeInicial = DateTime.Now.AddDays(-6)
            };

            var Regras = await _service.Buscar(queryParam);

            Assert.AreEqual(1, Regras.TotalItems);
        }

        [Test]
        [TestCase(TestName = "Teste Verificar Criar Regra Conflitante Negociacao",
                   Description = "Teste Verificar Criar Regra Conflitante Negociacao no Banco")]
        public async Task TesteVerificarConflitanteRegraNegociacaoValido()
        {
            var criarViewModel = _criarViewModel;

            var conflito = await _service.VerificarRegraConflitante(_criarViewModel);

            Assert.AreEqual(null, conflito);
        }
    }
}
