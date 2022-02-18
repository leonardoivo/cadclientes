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

namespace Tiradentes.CobrancaAtiva.Unit.ModalidadeTestes
{
    public class BuscarPorInstituicao
    {
        private CobrancaAtivaDbContext _context;
        private IModalidadeService _service;
        private ModalidadeModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ModalidadeTestes2")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);

            IdAlunoRepository idAlunoRepository = new IdAlunoRepository(_context);
            ParcelaTituloRepository parcelaTituloRepository = new ParcelaTituloRepository(_context);
            IModalidadeRepository repository = new ModalidadeRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ModalidadeService(repository, _mapper);

            _model = new ModalidadeModel
            {
                Modalidade = "Presencial",        
                CodigoMagister = "123"
            };

            if(_context.Modalidade.CountAsync().Result == 0)
            {
                _context.Modalidade.Add(_model);
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
        [TestCase(TestName = "Teste Buscar Modalidade Por Instituicao inválido",
                   Description = "Teste Buscar Modalidade Por Instituicao no Banco")]
        public async Task TesteBuscarPorInstituicaoInvalido()
        {
            var Modalidades = await _service.BuscarPorInstituicao(123);

            Assert.AreEqual(0, Modalidades.Count);
        }
    }
}
