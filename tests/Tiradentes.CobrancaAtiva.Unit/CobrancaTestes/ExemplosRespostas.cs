using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using Moq;
using System.Linq;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;

namespace Tiradentes.CobrancaAtiva.Unit.CobrancaTestes
{
    public class ExemplosRespostas
    {
        private CobrancaAtivaDbContext _context;
        private ICobrancaService _service;
        private IMapper _mapper;
        private Mock<ICobrancaRepository> _cobrancaRepository;
        private Mock<IAlunosInadimplentesRepository> _alunosInadimplentesRepository;
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
                    .UseInMemoryDatabase("CobrancaTests")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);

            _cobrancaRepository = new Mock<ICobrancaRepository>();
            _alunosInadimplentesRepository = new Mock<IAlunosInadimplentesRepository>();
            _regraNegociacaoRepository = new RegraNegociacaoRepository(_context);
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

        [Test]
        [TestCase(TestName = "Teste Exemplos Respostas",
                   Description = "Teste Exemplos Respostas")]
        public void TesteExemplosRespostasValido()
        {
            var exemplos = _service.ExemplosRespostas();

            Assert.GreaterOrEqual(exemplos.Count(), 3);
        }
    }
}
