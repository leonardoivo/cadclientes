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

namespace Tiradentes.CobrancaAtiva.Unit.ParametroEnvioTestes
{
    public class EnviarParametroParaConsumer
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

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("EnviarParametroParaConsumer")
                    .Options;
            
             DbContextOptions<CobrancaAtivaScfDbContext> optionsContextScf =
                new DbContextOptionsBuilder<CobrancaAtivaScfDbContext>()
                    .UseInMemoryDatabase("EnviarParametroParaConsumer2")
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

            _CriarInstituicaoModel = new InstituicaoModel()
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

            var CriarCursosModel = new CursoModel()
            {
                Descricao = "teste3",
                CodigoMagister = "aaa"
            };

            var CriarTituloAvulsoModel = new TituloAvulsoModel()
            {
                CodigoGT = 1,
                Descricao = "aaa",
            };

            var CriarSituacaoAlunoModel = new SituacaoAlunoModel()
            {
                Situacao = "aaa",
                CodigoMagister = "aaa",
            };

            var CriarTipoTituloModel = new TipoTituloModel()
            {
                TipoTitulo = "aaa",
                CodigoMagister = "aaa"
            };

            _context.Instituicao.Add(_CriarInstituicaoModel);
            _context.Modalidade.Add(CriarModalidadeModel);
            _context.EmpresaParceira.Add(CriarEmpresaParceiraModel);
            _context.Curso.Add(CriarCursosModel);
            _context.TituloAvulso.Add(CriarTituloAvulsoModel);
            _context.SituacaoAluno.Add(CriarSituacaoAlunoModel);
            _context.TipoTitulo.Add(CriarTipoTituloModel);
           
            _context.SaveChanges();

            _CriarParametroEnvio = new CriarParametroEnvioViewModel()
            {
                EmpresaParceiraId = 1,
                InstituicaoId = _CriarInstituicaoModel.Id,
                ModalidadeId = CriarModalidadeModel.Id,
                DiaEnvio = 28,
                Status = true,
                InadimplenciaInicial = DateTime.Now,
                InadimplenciaFinal = DateTime.Now,
                ValidadeInicial = DateTime.Now,
                ValidadeFinal = DateTime.Now,
                CursoIds = new int[1]{ CriarCursosModel.Id },
                SituacaoAlunoIds = new int[1]{ CriarSituacaoAlunoModel.Id },
                TituloAvulsoIds = new int[1]{ CriarTituloAvulsoModel.Id },
                TipoTituloIds = new int[1]{ CriarTipoTituloModel.Id }
            };

            
                _model = _mapper.Map<ParametroEnvioModel>(_CriarParametroEnvio);
                _context.ParametroEnvio.Add(_model);
                _context.SaveChanges();

        }

        [Test]
        [TestCase(TestName = "Teste Enviar para consumer Parametro envio",
                    Description = "Testando função de Enviar Para Consumer do CRUD Parametro envio")]
        public async Task TesteEnviarParaConsumerParametroEnvio()
        {
            await _service.EnviarParametroParaConsumer(_model.Id);
            Assert.Pass();
        }       
    } 
}