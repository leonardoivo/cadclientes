using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Api.Controllers;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Application.Configuration;
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
            var optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests")
                    .Options;
            var encryptationConfig = Options.Create(new EncryptationConfig()
            {
                BaseUrl = "http://teste.com/",
                DecryptAuthorization = "123",
                EncryptAuthorization = "123"
            });
            _context = new CobrancaAtivaDbContext(optionsContext);
            IEmpresaParceiraRepository repository = new EmpresaParceiraRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            var criptografiaService = new CriptografiaService(encryptationConfig, null);
            _service = new EmpresaParceiraService(repository, mapper, criptografiaService);
            _controller = new EmpresaParceiraController(_service);

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
            Assert.AreEqual(0, t.Value.Items.Count);
            Assert.AreEqual(0, t.Value.TotalItems);
            Assert.AreEqual(0, t.Value.TotalPaginas);
            Assert.AreEqual(1, t.Value.PaginaAtual);
            Assert.AreEqual(10, t.Value.TamanhoPagina);
        }
    }
}
