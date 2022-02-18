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
    public class ParcelaPagaInstituicao
    {
        private CobrancaAtivaDbContext _context;
        private IParcelaPagaAlunoInstituicaoService _service;
        private ParcelaPagaAlunoInstituicaoModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ParcelaPagaAlunoInstituicaoTestes")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);

            IdAlunoRepository idAlunoRepository = new IdAlunoRepository(_context);
            IParcelaPagaAlunoInstituicaoRepository repository = new ParcelaPagaAlunoInstituicaoRepository(idAlunoRepository, _context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ParcelaPagaAlunoInstituicaoService(repository);
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste parcela paga instituicao válido",
                   Description = "Teste parcela paga instituicao no banco")]
        public void TesteParcelaPagaInstituicaoValido()
        {
            Assert.AreEqual(false, _service.ParcelaPagaInstituicao("Bicicleta", "Bicileta", 10, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1));
        }

        [Test]
        [TestCase(TestName = "Teste parcela paga instituicao inválido",
                   Description = "Teste parcela paga instituicao no banco")]
        public void TesteParcelaPagaInstituicaoInvalido()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.ParcelaPagaInstituicao("P", "S", 10, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            Assert.Throws<NullReferenceException>(() => _service.ParcelaPagaInstituicao("P", "E", 10, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            Assert.Throws<NullReferenceException>(() => _service.ParcelaPagaInstituicao("P", "P", 10, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            Assert.Throws<NullReferenceException>(() => _service.ParcelaPagaInstituicao("P", "I", 10, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            Assert.Throws<NullReferenceException>(() => _service.ParcelaPagaInstituicao("P", "X", 10, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            Assert.Throws<NullReferenceException>(() => _service.ParcelaPagaInstituicao("T", "T", 10, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            Assert.Throws<NullReferenceException>(() => _service.ParcelaPagaInstituicao("C", "C", 10, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1));
        }
    }
}
