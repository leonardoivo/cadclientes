using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Api.Controllers;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;

namespace Tiradentes.CobrancaAtiva.Unit.EmpresaParceiraTestes
{
    public class ConsultaEmpresaParceira
    {
        private EmpresaParceiraController _controller;
        private CobrancaAtivaDbContext _context;
        private IEmpresaParceiraService _service;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);
            IEmpresaParceiraRepository repository = new EmpresaParceiraRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            IEmpresaParceiraService service = new EmpresaParceiraService(repository, null, mapper);
            _controller = new EmpresaParceiraController(service);

            _context.EmpresaParceira.Remove(_context.EmpresaParceira.FirstAsync().Result);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Consultar Empresa Parceira",
                    Description = "Teste usando um banco vazio e não enviado dados de paginação")]
        public async Task TesteBuscarTodos()
        {
            var t = await _controller.Buscar(new ConsultaEmpresaParceiraQueryParam());
            Assert.AreEqual(t.Value.Items.Count, 0);
            Assert.AreEqual(t.Value.TotalItems, 0);
            Assert.AreEqual(t.Value.TotalPaginas, 0);
            Assert.AreEqual(t.Value.PaginaAtual, 1);
            Assert.AreEqual(t.Value.TamanhoPagina, 10);
        }
    }
}
