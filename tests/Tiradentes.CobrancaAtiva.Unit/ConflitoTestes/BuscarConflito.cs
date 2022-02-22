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
using Tiradentes.CobrancaAtiva.Application.QueryParams;

namespace Tiradentes.CobrancaAtiva.Unit.ConflitoTestes
{
    public class BuscarConflito
    {
        private CobrancaAtivaDbContext _context;
        private IConflitoService _service;
        private ConflitoModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ConflitoTestes")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);

            IdAlunoRepository idAlunoRepository = new IdAlunoRepository(_context);
            ParcelaTituloRepository parcelaTituloRepository = new ParcelaTituloRepository(_context);
            IConflitoRepository repository = new ConflitoRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ConflitoService(repository, _mapper);

            _model = new ConflitoModel
            {
                Lote = "Lote",
                NomeLote = "NomeLote",
                EmpresaParceiraTentativaId = 1,
                EmpresaParceiraEnvioId = 1,
                Matricula = 1,
                NomeAluno = "NomeAluno",
                CPF = "CPF"
            };
            if(_context.ConflitoModel.CountAsync().Result == 0)
            {
                _context.ConflitoModel.Add(_model);
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
        [TestCase(TestName = "Teste Buscar Conflito válido",
                   Description = "Teste Buscar Conflito no Banco")]
        public async Task TesteBuscarValido()
        {
            var queryParam = new ConsultaConflitoQueryParam()
            {
                EmpresasParceiraTentativa = new int[1]{ _model.Id },
                EmpresasParceiraEnvio = new int[1]{ _model.Id },
                NomeLote = "NomeLote",
                NomeAluno = "NomeAluno",
                CPF = "CPF"
            };
            
            var Conflitos = await _service.Buscar(queryParam);

            Assert.AreEqual(1, Conflitos.TotalItems);
        }
    }
}
