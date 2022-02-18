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
using System;

namespace Tiradentes.CobrancaAtiva.Unit.ParcelaPagaAlunoIntituicaoTestes
{
    public class MatriculaAlunoExiste
    {
        private CobrancaAtivaDbContext _context;
        private IMatriculaAlunoExisteService _service;
        private MatriculaAlunoExisteModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("MatriculaAlunoExisteTestes")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);

            IMatriculaAlunoExisteRepository repository = new MatriculaAlunoExisteRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new MatriculaAlunoExisteService(repository);
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste matricula aluno existe válido",
                   Description = "Teste matricula aluno existe no banco")]
        public void TesteMatriculaAlunoExisteValido()
        {
            Assert.AreEqual(false, _service.MatriculaAlunoExiste("Bicicleta", "Bicileta", 10));
        }

        [Test]
        [TestCase(TestName = "Teste matricula aluno existe inválido",
                   Description = "Teste matricula aluno existe no banco")]
        public void TesteMatriculaAlunoExisteInvalido()
        {
            Assert.Throws<NullReferenceException>(() => _service.MatriculaAlunoExiste("P", "S", 10));
            Assert.Throws<NullReferenceException>(() => _service.MatriculaAlunoExiste("P", "E", 10));
            Assert.Throws<NullReferenceException>(() => _service.MatriculaAlunoExiste("P", "P", 10));
            Assert.Throws<NullReferenceException>(() => _service.MatriculaAlunoExiste("P", "I", 10));
            Assert.Throws<NullReferenceException>(() => _service.MatriculaAlunoExiste("P", "X", 10));
            Assert.Throws<NullReferenceException>(() => _service.MatriculaAlunoExiste("T", "T", 10));
            Assert.Throws<NullReferenceException>(() => _service.MatriculaAlunoExiste("C", "C", 10));
            Assert.Throws<NullReferenceException>(() => _service.MatriculaAlunoExiste("R", "R", 10));
        }
    }
}
