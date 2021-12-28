using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Api.Controllers;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;

namespace Tiradentes.CobrancaAtiva.Unit.EmpresaParceiraTestes
{
    public class ConsultaEmpresaParceiraPorId
    {
        private EmpresaParceiraController _controller;
        private CobrancaAtivaDbContext _context;
        private IEmpresaParceiraService _service;
        private IOptions<EncryptationConfig> _encryptationConfig;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests")
                    .Options;
            _encryptationConfig = Options.Create<EncryptationConfig>(new EncryptationConfig()
            {
                BaseUrl = "https://encrypt-service-2kcoisahga-ue.a.run.app/",
                DecryptAuthorization = "bWVjLWVuYzpwYXNzd29yZA==",
                EncryptAuthorization = "bWVjLWRlYzpwYXNzd29yZA=="
            });
            _context = new CobrancaAtivaDbContext(optionsContext);
            IEmpresaParceiraRepository repository = new EmpresaParceiraRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            IEmpresaParceiraService service = new EmpresaParceiraService(repository, mapper, _encryptationConfig);
            _controller = new EmpresaParceiraController(service);
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Consultar Empresa Parceira por id",
                   Description = "Teste buscando um dado existente no banco")]
        public async Task TesteBuscarPorIdComResultado()
        {
            var model = new EmpresaParceiraModel(
                NomeFantasia: "Nome Fantasia",
                RazaoSocial: null,
                Sigla: null,
                CNPJ: null,
                NumeroContrato: null,
                URL: null,
                Status: true
            );

            await InserirDadoNoBanco(model);

            var t = await _controller.Buscar(model.Id);

            Assert.AreEqual(t.Value.Id, model.Id);
            Assert.AreEqual(t.Value.NomeFantasia, model.NomeFantasia);

            await DeletarTodasEmpresasParceirasBanco();
        }

        [Test]
        [TestCase(TestName = "Teste Consultar Empresa Parceira por id inexistente ",
                    Description = "Teste buscando um dado inexistente")]
        public async Task TesteBuscarPorId()
        {
            var model = new EmpresaParceiraModel(
                NomeFantasia: "Nome Fantasia",
                RazaoSocial: null,
                Sigla: null,
                CNPJ: null,
                NumeroContrato: null,
                URL: null,
                Status: true
            );

            await InserirDadoNoBanco(model);

            var t = await _controller.Buscar(++model.Id);

            Assert.AreEqual(t.Value, null);
        }

        private async Task DeletarTodasEmpresasParceirasBanco()
        {
            _context.EmpresaParceira.RemoveRange(await _context.EmpresaParceira.ToListAsync());
            await _context.SaveChangesAsync();
        }

        private async Task InserirDadoNoBanco(EmpresaParceiraModel model)
        {
            _context.EmpresaParceira.Add(model);
            await _context.SaveChangesAsync();
        }
    }
}
