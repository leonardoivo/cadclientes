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
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Api.Controllers;

namespace Tiradentes.CobrancaAtiva.Unit.ParametroEnvio
{
    public class ConsultaParametroEnvioPorId
    {
        private CobrancaAtivaDbContext _context;
        private CobrancaAtivaScfDbContext _contextScf;
        private MongoContext _contextMongo;
        private IParametroEnvioService _service;
        private IOptions<EncryptationConfig> _encryptationConfig;
        private ParametroEnvioModel _model;
        private IMapper _mapper;
        private Mock<IAlunosInadimplentesRepository> _alunosInadimplentesRepository;
        private Mock<ILoteEnvioRepository> _loteEnvioRepository;
        private CriarParametroEnvioViewModel _CriarParametroEnvio;
        private CursoModel _CriarCursoModel;
        private TituloAvulsoModel _CriarTituloAvulsoModel;
        private SituacaoAlunoModel _CriarSituacaoAlunoModel;
        private TipoTituloModel _CriarTipoTituloModel;


        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests2")
                    .Options;
            
             DbContextOptions<CobrancaAtivaScfDbContext> optionsContextScf =
                new DbContextOptionsBuilder<CobrancaAtivaScfDbContext>()
                    .UseInMemoryDatabase("SCF2")
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


        }

        [TearDown]
        public void TearDown()
        {
            GC.SuppressFinalize(this);
        }

        [Test]
        [TestCase(TestName = "Teste Consultar Parametro envio por ID",
                    Description = "Testando função de busca do CRUD Parametro envio")]
        public async Task TesteBuscarParametroEnvio()
        {

                var busca = await _service.BuscarPorId(_model.Id);

                Assert.AreEqual(_model.DiaEnvio, busca?.DiaEnvio);
        }
    } 
}