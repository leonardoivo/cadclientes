using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
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
        private Mock<HttpMessageHandler> _mockHttpClient;

        [SetUp]
        public void Setup()
        {
            var optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests")
                    .Options;
            var encryptationConfig = Options.Create(new EncryptationConfig()
            {
                DecryptAuthorization = "123",
                EncryptAuthorization = "123"
            });
            _context = new CobrancaAtivaDbContext(optionsContext);
            IEmpresaParceiraRepository repository = new EmpresaParceiraRepository(_context);
            IMapper mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _mockHttpClient = new Mock<HttpMessageHandler>();
            var client = new HttpClient(_mockHttpClient.Object);
            client.BaseAddress = new Uri("http://teste.com/");
            var criptografiaService =
                new CriptografiaService(encryptationConfig, client);
            _service = new EmpresaParceiraService(repository, mapper, criptografiaService);
            _controller = new EmpresaParceiraController(_service);
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
                Status: true,
                ChaveIntegracaoSap: null
            );

            await InserirDadoNoBanco(model);

            var senhaDescriptografada = "teste-senha-descriptgrafada";
            var httpResponse = new HttpResponseMessage
                {StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonSerializer.Serialize(senhaDescriptografada))};
            _mockHttpClient.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var t = await _controller.Buscar(model.Id);

            Assert.AreEqual(t.Value.Id, model.Id);
            Assert.AreEqual(t.Value.NomeFantasia, model.NomeFantasia);
            Assert.AreEqual(t.Value.SenhaApi, senhaDescriptografada);

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
                Status: true,
                ChaveIntegracaoSap: null
            );
            
            var senhaDescriptografada = "teste-senha-descriptgrafada";
            var httpResponse = new HttpResponseMessage
                {StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonSerializer.Serialize(senhaDescriptografada))};
            _mockHttpClient.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            await InserirDadoNoBanco(model);

            var t = await _controller.Buscar(++model.Id);

            Assert.AreEqual(null, t.Value);
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