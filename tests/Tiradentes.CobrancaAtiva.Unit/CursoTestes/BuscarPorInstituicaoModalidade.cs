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
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Unit.CursoTestes
{
    public class BuscarPorInstituicaoModalidade
    {
        private CobrancaAtivaDbContext _context;
        private ICursoService _service;
        private CursoModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CursoTestes2")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);

            IdAlunoRepository idAlunoRepository = new IdAlunoRepository(_context);
            ParcelaTituloRepository parcelaTituloRepository = new ParcelaTituloRepository(_context);
            ICursoRepository repository = new CursoRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new CursoService(repository, _mapper);

            _model = new CursoModel
            {
                Descricao = "teste",
                ModalidadeId = 1,
                InstituicaoId = 1,
                CodigoMagister = "1",
            };

            if(_context.Curso.CountAsync().Result == 0)
            {
                _context.Curso.Add(_model);
                _context.SaveChanges();
            }
            
            _context.ChangeTracker.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Buscar Curso Por Instituicao Modalidade inválido",
                   Description = "Teste Buscar Curso Por Instituicao Modalidade no Banco")]
        public async Task TesteBuscarPorInstituicaoModalidadeInvalido()
        {
            var Curso = await _service.BuscarPorInstituicaoModalidade(_model.InstituicaoId, _model.ModalidadeId);

            Assert.AreEqual(0, Curso.Count);
        }
    }
}
