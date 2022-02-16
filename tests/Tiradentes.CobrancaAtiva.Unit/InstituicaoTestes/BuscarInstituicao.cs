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

namespace Tiradentes.CobrancaAtiva.Unit.InstituicaoTestes
{
    public class BuscarInstituicao
    {
        private CobrancaAtivaDbContext _context;
        private IInstituicaoService _service;
        private InstituicaoModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("InstituicaoTestes")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            IInstituicaoRepository repository = new InstituicaoRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new InstituicaoService(repository, _mapper);

            _model = new InstituicaoModel
            {
                Instituicao = "UNIT"
            };

            if(_context.Instituicao.CountAsync().Result == 0)
            {
                _context.Instituicao.Add(_model);
                _context.SaveChanges();
            }
            
            _context.ChangeTracker.Clear();
        }

        [Test]
        [TestCase(TestName = "Teste Buscar intituição válido",
                   Description = "Teste Buscar intituição no Banco")]
        public async Task TesteBuscarInstituicaoValido()
        {
            var Instituicoes = await _service.Buscar();

            Assert.AreEqual(1, Instituicoes.Count);
        }
    }
}
