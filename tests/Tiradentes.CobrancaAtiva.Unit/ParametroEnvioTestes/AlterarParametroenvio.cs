using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using Moq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Tiradentes.CobrancaAtiva.Unit.ParametroEnvioTestes
{
    public class AlterarParametroEnvio
    {
        private CobrancaAtivaDbContext _context;
        private CobrancaAtivaScfDbContext _contextScf;
        private IParametroEnvioService _service;
        private IOptions<EncryptationConfig> _encryptationConfig;
        private IMapper _mapper;
        private Mock<IAlunosInadimplentesRepository> _alunosInadimplentesRepository;
        private Mock<ILoteEnvioRepository> _loteEnvioRepository;
        private CriarParametroEnvioViewModel _CriarParametroEnvio;
        private CursoModel _CriarCursoModel;
        private TituloAvulsoModel _CriarTituloAvulsoModel;
        private SituacaoAlunoModel _CriarSituacaoAlunoModel;
        private TipoTituloModel _CriarTipoTituloModel;
        private CacheServiceRepository _cacheServiceRepository;
        private Mock<HttpMessageHandler> _mockHttpClient;
        private ParametroEnvioModel _model;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("TesteAlterarParametro")
                    .Options;
            
             DbContextOptions<CobrancaAtivaScfDbContext> optionsContextScf =
                new DbContextOptionsBuilder<CobrancaAtivaScfDbContext>()
                    .UseInMemoryDatabase("TesteAlterarParametro")
                    .Options;
       
            _encryptationConfig = Options.Create<EncryptationConfig>(new EncryptationConfig()
                {
                    BaseUrl = "https://encrypt-service-2kcoisahga-ue.a.run.app/",
                    DecryptAuthorization = "bWVjLWVuYzpwYXNzd29yZA==",
                    EncryptAuthorization = "bWVjLWRlYzpwYXNzd29yZA=="
                });

            _mockHttpClient = new Mock<HttpMessageHandler>();
            var client = new HttpClient(_mockHttpClient.Object);
            client.BaseAddress = new Uri("http://teste.com/");
            var criptografiaService = new CriptografiaService(_encryptationConfig, client);

            _alunosInadimplentesRepository = new Mock<IAlunosInadimplentesRepository>();
            _loteEnvioRepository = new Mock<ILoteEnvioRepository>();
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _context = new CobrancaAtivaDbContext(optionsContext);
            _contextScf = new CobrancaAtivaScfDbContext(optionsContextScf);
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
                options.UseInMemoryDatabase("TesteAlterarParametro")); 
            services.AddDbContext<CobrancaAtivaScfDbContext>(options =>
                options.UseInMemoryDatabase("TesteAlterarParametro"));

            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService(typeof(CobrancaAtivaDbContext));
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider);
            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory.Setup(x => x.CreateScope()).Returns(serviceScope.Object);

            _cacheServiceRepository = new CacheServiceRepository(serviceScopeFactory.Object);

            IParametroEnvioRepository repository = new ParametroEnvioRepository(_cacheServiceRepository, _context);
            IEmpresaParceiraRepository empresaParceiraRepository = new EmpresaParceiraRepository(_context);
            IGeracaoCobrancasRepository geracaoCobrancasRepository = new GeracaoCobrancasRepository(_contextScf);
            IItensGeracaoRepository itensGeracaoRepository = new ItensGeracaoRepository(_context);
            IArquivoCobrancasRepository arquivoCobrancasRepository= new ArquivoCobrancasRepository(_contextScf);
            IConflitoRepository conflitoRepository = new ConflitoRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            IOptions<RabbitMQConfig> rabbitOptions = Options.Create<RabbitMQConfig>(new RabbitMQConfig() { 
                Queue = "consulta_facilitada_dev",
                QueueEnvioArquivo = "envio-arquivo-lote-dev",
                HostName = "fox-02.rmq.cloudamqp.com",
                VirtualHost = "lxytdtks",
                UserName = "lxytdtks",
                Password = "YXoTIx-qdbPYJRwt8HUDgBsFgsoczRtu"
            });

            _service = new ParametroEnvioService(criptografiaService, repository, geracaoCobrancasRepository, itensGeracaoRepository, _alunosInadimplentesRepository.Object, _loteEnvioRepository.Object, conflitoRepository, mapper, rabbitOptions);

            _CriarCursoModel = new CursoModel()
            {
               Descricao = "aaa",
               ModalidadeId = 1,
               InstituicaoId = 10,
               CodigoMagister = "bbb"
            };
            
            _CriarTituloAvulsoModel = new TituloAvulsoModel()
            {
                CodigoGT = 1,
                Descricao = "aaa",
            };

            _CriarSituacaoAlunoModel = new SituacaoAlunoModel()
            {
                Situacao = "aaa",
                CodigoMagister = "bbb",
            };

            _CriarTipoTituloModel = new TipoTituloModel()
            {
                TipoTitulo = "aaa",
                CodigoMagister = "bbb"
            };

            
            var CriarInstituicaoModel = new InstituicaoModel()
            {
                Instituicao = "teste2"
            };

            var CriarModalidadeModel = new ModalidadeModel()
            {
                Modalidade = "teste2"
            };

            var CriarEmpresaParceiraModel = new EmpresaParceiraModel()
            {
                ChaveIntegracaoSap = "teste2"
            };

            
            
            _context.Curso.Add(_CriarCursoModel);
            _context.TituloAvulso.Add(_CriarTituloAvulsoModel);
            _context.SituacaoAluno.Add(_CriarSituacaoAlunoModel);
            _context.TipoTitulo.Add(_CriarTipoTituloModel);
            _context.Instituicao.Add(CriarInstituicaoModel);
            _context.Modalidade.Add(CriarModalidadeModel);
            _context.EmpresaParceira.Add(CriarEmpresaParceiraModel);
           
            _context.SaveChanges();

            _CriarParametroEnvio = new CriarParametroEnvioViewModel()
            {
            EmpresaParceiraId = CriarEmpresaParceiraModel.Id,
            InstituicaoId = CriarInstituicaoModel.Id,
            ModalidadeId = CriarModalidadeModel.Id,
            DiaEnvio = 28,
            Status = true,
            InadimplenciaInicial = DateTime.Now,
            InadimplenciaFinal = DateTime.Now,
            ValidadeInicial = DateTime.Now,
            ValidadeFinal = DateTime.Now,
            CursoIds = new int[1]{ _CriarCursoModel.Id },
            SituacaoAlunoIds = new int[1]{ _CriarSituacaoAlunoModel.Id },
            TituloAvulsoIds = new int[1]{ _CriarTituloAvulsoModel.Id },
            TipoTituloIds = new int[1]{ _CriarTipoTituloModel.Id }
            };

            _model = _mapper.Map<ParametroEnvioModel>(_CriarParametroEnvio);
            _context.ParametroEnvio.Add(_model);
            _context.SaveChanges();


             _context.ChangeTracker.Clear();


        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Parametro envio",
                    Description = "Testando função de Atualizar do CRUD Parametro envio")]
        public async Task TesteAlterarParametroEnvio()
        {
            var Alterar = new AlterarParametroEnvioViewModel()
            {
                DiaEnvio = 10,
                Id = _model.Id
            };

            var teste = await _service.Alterar(Alterar);

            Assert.AreEqual(Alterar.DiaEnvio, teste?.DiaEnvio);
        }
    } 
}