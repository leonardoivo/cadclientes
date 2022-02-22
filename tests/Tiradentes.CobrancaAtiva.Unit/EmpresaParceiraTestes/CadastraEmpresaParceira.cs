using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Api.Controllers;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.Utils;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Tiradentes.CobrancaAtiva.Application.Configuration;

namespace Tiradentes.CobrancaAtiva.Unit.EmpresaParceiraTestes
{
    public class CadastraEmpresaParceira
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

            if (_context.EmpresaParceira.CountAsync().Result <= 0) return;
            _context.EmpresaParceira.Remove(_context.EmpresaParceira.FirstAsync().Result);
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            _service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Empresa Parceira",
            Description = "Teste cadastrar Empresa Parceira no Banco")]
        public async Task TesteCadastrarValido()
        {
            var model = new EmpresaParceiraViewModel
            {
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = "97355899000105",
                NumeroContrato = "NumeroContrato",
                ChaveIntegracaoSap = "a1b2c3d4",
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel>
                {
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
            };

            var senhaDescriptografada = "teste-senha-descriptgrafada";
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(senhaDescriptografada))
            };
            _mockHttpClient.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var empresa = await _service.Criar(model);

            Assert.IsNotNull(empresa.Id);
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Empresa Parceira Sem Nome Fantasia",
            Description = "Teste cadastrar Empresa Parceira no Banco")]
        public void TesteCadastrarNomeFantasiaNull()
        {
            var model = new EmpresaParceiraViewModel
            {
                NomeFantasia = null,
                RazaoSocial = "Razao Social",
                CNPJ = "28.992.700/0001-29",
                NumeroContrato = "NumeroContrato",
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel>
                {
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Criar(model));
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Empresa Parceira Sem Razao Social",
            Description = "Teste cadastrar Empresa Parceira no Banco")]
        public void TesteCadastrarRazaoSocialNull()
        {
            var model = new EmpresaParceiraViewModel
            {
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = null,
                CNPJ = "28.992.700/0001-29",
                NumeroContrato = "NumeroContrato",
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel>
                {
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Criar(model));
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Empresa Parceira Sem CNPJ",
            Description = "Teste cadastrar Empresa Parceira no Banco")]
        public void TesteCadastrarCnpjNull()
        {
            var model = new EmpresaParceiraViewModel
            {
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = null,
                NumeroContrato = "NumeroContrato",
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel>
                {
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Criar(model));
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Empresa Parceira CNPJ inválido",
            Description = "Teste cadastrar Empresa Parceira no Banco")]
        public void TesteCadastrarCnpjInvalido()
        {
            var model = new EmpresaParceiraViewModel
            {
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = "CNPJ",
                NumeroContrato = "NumeroContrato",
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel>
                {
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Criar(model));
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Empresa Parceira Sem Numero Contrato",
            Description = "Teste cadastrar Empresa Parceira no Banco")]
        public void TesteCadastrarNumeroContratoNull()
        {
            var model = new EmpresaParceiraViewModel
            {
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = "28.992.700/0001-29",
                NumeroContrato = null,
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel>
                {
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Criar(model));
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Empresa Parceira Sem Contatos",
            Description = "Teste cadastrar Empresa Parceira no Banco")]
        public void TesteCadastrarNumeroContatoNull()
        {
            var model = new EmpresaParceiraViewModel
            {
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = "28.992.700/0001-29",
                NumeroContrato = null,
                Contatos = null
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Criar(model));
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Empresa Parceira Mais de 3 Contatos",
            Description = "Teste cadastrar Empresa Parceira no Banco")]
        public void TesteCadastrarNumeroMaisContatosNull()
        {
            var model = new EmpresaParceiraViewModel
            {
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = "28.992.700/0001-29",
                NumeroContrato = null,
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel>
                {
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    },
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    },
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    },
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Criar(model));
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Empresa Parceira Sem Chave Integração SAP",
            Description = "Teste cadastrar Empresa Parceira no Banco")]
        public void TesteCadastrarChaveIntegracaoSapNull()
        {
            var model = new EmpresaParceiraViewModel
            {
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = "28.992.700/0001-29",
                NumeroContrato = "NumeroDoContrato",
                ChaveIntegracaoSap = null,
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel>
                {
                    new ContatoEmpresaParceiraViewModel
                    {
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Criar(model));
        }
    }
}
