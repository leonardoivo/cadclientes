using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NUnit.Framework;
using System;
using Moq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
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
    public class ConsultaParametroEnvio
    {
        private CobrancaAtivaDbContext _context;
        private CobrancaAtivaScfDbContext _contextScf;
        private IParametroEnvioService _service;
        private IOptions<EncryptationConfig> _encryptationConfig;
        private ParametroEnvioModel _model;
        private IMapper _mapper;
        private Mock<IAlunosInadimplentesRepository> _alunosInadimplentesRepository;
        private Mock<ILoteEnvioRepository> _loteEnvioRepository;
        private CriarParametroEnvioViewModel _CriarParametroEnvio;
        private InstituicaoModel _CriarInstituicaoModel;
        private Mock<HttpMessageHandler> _mockHttpClient;
        private CacheServiceRepository _cacheServiceRepository;
        private ModalidadeModel _CriarModalidadeModel;
        private EmpresaParceiraModel _CriarEmpresaParceiraModel;
        private CursoModel _CriarCursoModel;
        private TituloAvulsoModel _CriarTituloAvulsoModel;
        private SituacaoAlunoModel _CriarSituacaoAlunoModel;
        private TipoTituloModel _CriarTipoTituloModel;

       [SetUp]
       public void Setup()
       {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("TesteConsultaParametro")
                    .Options;
            
             DbContextOptions<CobrancaAtivaScfDbContext> optionsContextScf =
                new DbContextOptionsBuilder<CobrancaAtivaScfDbContext>()
                    .UseInMemoryDatabase("TesteConsultaParametro")
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
                options.UseInMemoryDatabase("TesteConsultaParametro")); 
            services.AddDbContext<CobrancaAtivaScfDbContext>(options =>
                options.UseInMemoryDatabase("TesteConsultaParametro"));

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

            _service = new ParametroEnvioService(criptografiaService, repository, geracaoCobrancasRepository, itensGeracaoRepository, _alunosInadimplentesRepository.Object, _loteEnvioRepository.Object, conflitoRepository, mapper, rabbitOptions, arquivoCobrancasRepository);

            _CriarInstituicaoModel = new InstituicaoModel()
            {
                Instituicao = "teste2"
            };

            _CriarModalidadeModel = new ModalidadeModel()
            {
                Modalidade = "teste2"
            };

            _CriarEmpresaParceiraModel = new EmpresaParceiraModel()
            {
                ChaveIntegracaoSap = "teste2"
            };

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


            _context.Curso.Add(_CriarCursoModel);
            _context.TituloAvulso.Add(_CriarTituloAvulsoModel);
            _context.SituacaoAluno.Add(_CriarSituacaoAlunoModel);
            _context.TipoTitulo.Add(_CriarTipoTituloModel);
            _context.Instituicao.Add(_CriarInstituicaoModel);
            _context.Modalidade.Add(_CriarModalidadeModel);
            _context.EmpresaParceira.Add(_CriarEmpresaParceiraModel);
           
            _context.SaveChanges();

            _CriarParametroEnvio = new CriarParametroEnvioViewModel()
            {
                EmpresaParceiraId = _CriarEmpresaParceiraModel.Id,
                InstituicaoId = _CriarInstituicaoModel.Id,
                ModalidadeId = _CriarModalidadeModel.Id,
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

               _CriarParametroEnvio.Status = false;

               _context.ParametroEnvio.Add(_mapper.Map<ParametroEnvioModel>(_CriarParametroEnvio));
               _context.SaveChanges();
           
        }

       [Test]
       [TestCase(TestName = "Teste Consultar Parametro envio",
                   Description = "Testando função de busca do CRUD Parametro envio")]
       public async Task TesteBuscarParametroEnvioValido()
       {
            
            var queryParam = new ConsultaParametroEnvioQueryParam(){
                Status = true,
                InstituicaoId = _CriarInstituicaoModel.Id,
                EmpresaId = _CriarEmpresaParceiraModel.Id
            };

            var busca = await _service.Buscar(queryParam);
            Assert.AreEqual(1, busca.TotalItems);

       }

       [Test]
       [TestCase(TestName = "Teste Consultar Parametro envio inválido",
                   Description = "Testando função de busca do CRUD Parametro envio")]
       public async Task TesteBuscarParametroEnvioInvalido()
       {
            
            var queryParam = new ConsultaParametroEnvioQueryParam(){
                EmpresaId = 1,
                InstituicaoId = 1,
                DiaEnvio = 1,
                Status = false,
                InadimplenciaInicial = DateTime.Now,
                InadimplenciaFinal = DateTime.Now,
                ValidadeInicial = DateTime.Now,
                ValidadeFinal = DateTime.Now,
                Modalidades = new int[1]{ 1 },
                Cursos = new int[1]{ 1 },
                TitulosAvulsos = new int[1]{ 1 },
                SituacoesAlunos = new int[1]{ 1 },
                TiposTitulos = new int[1]{ 1 }
            };

            var busca = await _service.Buscar(queryParam);
            Assert.AreEqual(0, busca.TotalItems);
       }       
    } 
}
