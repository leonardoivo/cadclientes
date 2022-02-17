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

namespace Tiradentes.CobrancaAtiva.Unit.ParametroEnvioTestes
{
    public class CriarParametroEnvio
    {
        private CobrancaAtivaDbContext _context;
        private CobrancaAtivaScfDbContext _contextScf;
        private IParametroEnvioService _service;
        private IOptions<EncryptationConfig> _encryptationConfig;
        private IMapper _mapper;
        private Mock<IAlunosInadimplentesRepository> _alunosInadimplentesRepository;
        private Mock<ILoteEnvioRepository> _loteEnvioRepository;
        private CriarParametroEnvioViewModel _CriarParametroEnvio;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests")
                    .Options;
            
             DbContextOptions<CobrancaAtivaScfDbContext> optionsContextScf =
                new DbContextOptionsBuilder<CobrancaAtivaScfDbContext>()
                    .UseInMemoryDatabase("SCF")
                    .Options;
       
            _encryptationConfig = Options.Create<EncryptationConfig>(new EncryptationConfig()
                {
                    BaseUrl = "https://encrypt-service-2kcoisahga-ue.a.run.app/",
                    DecryptAuthorization = "bWVjLWVuYzpwYXNzd29yZA==",
                    EncryptAuthorization = "bWVjLWRlYzpwYXNzd29yZA=="
                });

            _alunosInadimplentesRepository = new Mock<IAlunosInadimplentesRepository>();
            _loteEnvioRepository = new Mock<ILoteEnvioRepository>();
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _context = new CobrancaAtivaDbContext(optionsContext);
            _contextScf = new CobrancaAtivaScfDbContext(optionsContextScf);
            IParametroEnvioRepository repository = new ParametroEnvioRepository(_context);
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

            _service = new ParametroEnvioService(repository, empresaParceiraRepository, geracaoCobrancasRepository, itensGeracaoRepository, arquivoCobrancasRepository, _alunosInadimplentesRepository.Object, _loteEnvioRepository.Object, conflitoRepository, mapper, rabbitOptions, _encryptationConfig);

            _CriarParametroEnvio = new CriarParametroEnvioViewModel()
            {
                EmpresaParceiraId = 1,
                InstituicaoId = 10,
                ModalidadeId = 100,
                DiaEnvio = 28,
                Status = true,
                InadimplenciaInicial = DateTime.Now,
                InadimplenciaFinal = DateTime.Now,
                ValidadeInicial = DateTime.Now,
                ValidadeFinal = DateTime.Now,
                CursoIds = new int[1]{ 1 },
                SituacaoAlunoIds = new int[1]{ 1 },
                TituloAvulsoIds = new int[1]{ 1 },
                TipoTituloIds = new int[1]{ 1 }
            };
        }

        [Test]
        [TestCase(TestName = "Teste criar Parametro envio Por ID",
                    Description = "Testando função de criar do CRUD Parametro envio")]
        public async Task TesteCriarParametroEnvio()
        {
            var Criar = await _service.Criar(_CriarParametroEnvio);

            Assert.AreEqual(Criar.DiaEnvio, _CriarParametroEnvio.DiaEnvio);
        }
    } 
}