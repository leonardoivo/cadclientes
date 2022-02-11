using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tiradentes.CobrancaAtiva.Api.Controllers;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using Microsoft.Extensions.Options;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using System.Threading.Tasks;
using Moq;

namespace Tiradentes.CobrancaAtiva.Unit.CobrancaTestes
{
    public class ExemplosRespostas
    {
        private CobrancaController _controller;
        private CobrancaAtivaDbContext _context;
        private ICobrancaService _service;
        private IMapper _mapper;
        private IOptions<EncryptationConfig> _encryptationConfig;
        protected readonly IAlunosInadimplentesRepository _alunosInadimplentesRepository;
        protected readonly IRegraNegociacaoService _regraNegociacaoService;
        protected readonly IInstituicaoService _instituicaoService;
        protected readonly IParcelasAcordoService _parcelaService;
        protected readonly IModalidadeService _modalidadeService;
        protected readonly ICursoService _cursoService;
        protected readonly IParcelasAcordoService _parcelasAcordoService;
        protected readonly IAcordoCobrancaService _acordoCobrancaService;
        private Mock<ICobrancaRepository> _cobrancaRepository;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaTests")
                    .Options;
            _encryptationConfig = Options.Create<EncryptationConfig>(new EncryptationConfig()
            {
                BaseUrl = "https://encrypt-service-2kcoisahga-ue.a.run.app/",
                DecryptAuthorization = "bWVjLWVuYzpwYXNzd29yZA==",
                EncryptAuthorization = "bWVjLWRlYzpwYXNzd29yZA=="
            });
            _context = new CobrancaAtivaDbContext(optionsContext);
            _cobrancaRepository = new Mock<ICobrancaRepository>();
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new CobrancaService(
                            _cobrancaRepository.Object,
                _alunosInadimplentesRepository,
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

        [Test]
        [TestCase(TestName = "Teste Exemplos Respostas",
                   Description = "Teste Exemplos Respostas")]
        public void TesteExemplosRespostasValido()
        {
            var exemplos = _service.ExemplosRespostas();

            Assert.Pass();
        }
    }
}
