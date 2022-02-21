using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Services;

namespace Tiradentes.CobrancaAtiva.Unit.SituacaoAlunoTestes
{
    public class ConsultarSituacaoAluno
    {
        private CobrancaAtivaDbContext _context;
        private SituacaoAlunoService _service;
        private SituacaoAlunoModel _CriarSituacaoAlunoModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("SitutacaoAlunoTests")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            ISituacaoAlunoRepository repository = new SituacaoAlunoRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new SituacaoAlunoService(repository, mapper);


            _CriarSituacaoAlunoModel = new SituacaoAlunoModel()
            {
                Situacao = "AAA",
                CodigoMagister = "M4H"
            };

            _context.SituacaoAluno.Add(_CriarSituacaoAlunoModel);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Consultar Situacao Aluno",
                    Description = "Teste consultando rota de busca de Situacao Aluno")]
        public async Task TesteBuscarTodos()
        {
            var SituacaoAluno = await _service.Buscar();
            Assert.AreEqual(1, SituacaoAluno.Count);
        }
    }
}
