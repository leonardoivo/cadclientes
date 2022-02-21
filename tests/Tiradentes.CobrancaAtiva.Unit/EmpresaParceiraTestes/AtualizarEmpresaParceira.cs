using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tiradentes.CobrancaAtiva.Api.Controllers;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.Utils;
using Microsoft.Extensions.Options;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace Tiradentes.CobrancaAtiva.Unit.EmpresaParceiraTestes
{
    public class AtualizarEmpresaParceira
    {
        private EmpresaParceiraController _controller;
        private CobrancaAtivaDbContext _context;
        private IEmpresaParceiraService _service;
        private Mock<HttpMessageHandler> _mockHttpClient;
        private EmpresaParceiraViewModel _model;

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

            _model = new EmpresaParceiraViewModel
            {
                Id = 1,
                CEP = "42345234",
                Estado = "SE",
                Cidade = "Aracaju",
                Logradouro = "Rua Pedro",
                Numero = "7",
                Complemento = "",
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = "97355899000105",
                NumeroContrato = "NumeroContrato",
                Contatos = new List<ContatoEmpresaParceiraViewModel>
                {
                    new ContatoEmpresaParceiraViewModel
                    {
                        Id = 1,
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                },
                ChaveIntegracaoSap = "123423525"
            };

            if (_context.EmpresaParceira.CountAsync().Result == 0)
            {
                _context.EmpresaParceira.Add(mapper.Map<EmpresaParceiraModel>(_model));
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
        [TestCase(TestName = "Teste Atualizar Empresa Parceira",
            Description = "Teste Atualizar Empresa Parceira no Banco")]
        public void TesteAtualizarEmpresaParceira()
        {
            _model.NomeFantasia = "Mudança";

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

            Assert.IsTrue(_service.Atualizar(_model).IsCompleted);
        }


        [Test]
        [TestCase(TestName = "Teste Atualizar Empresa Parceira Sem Nome Fantasia",
            Description = "Teste Atualizar Empresa Parceira no Banco")]
        public void TesteAtualizarNomeFantasiaNull()
        {
            _model.NomeFantasia = null;

            Assert.ThrowsAsync<CustomException>(async () => await _service.Atualizar(_model));
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Empresa Parceira Sem Razao Social",
            Description = "Teste Atualizar Empresa Parceira no Banco")]
        public void TesteAtualizarRazaoSocialNull()
        {
            _model.RazaoSocial = null;

            Assert.ThrowsAsync<CustomException>(async () => await _service.Atualizar(_model));
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Empresa Parceira Sem CNPJ",
            Description = "Teste Atualizar Empresa Parceira no Banco")]
        public void TesteAtualizarCnpjNull()
        {
            _model.CNPJ = null;

            Assert.ThrowsAsync<CustomException>(async () => await _service.Atualizar(_model));
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Empresa Parceira CNPJ inválido",
            Description = "Teste Atualizar Empresa Parceira no Banco")]
        public void TesteAtualizarCnpjInvalido()
        {
            _model.CNPJ = "CNPJ";

            Assert.ThrowsAsync<CustomException>(async () => await _service.Atualizar(_model));
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Empresa Parceira Sem Numero Contrato",
            Description = "Teste Atualizar Empresa Parceira no Banco")]
        public void TesteAtualizarNumeroContratoNull()
        {
            _model.NumeroContrato = null;

            Assert.ThrowsAsync<CustomException>(async () => await _service.Atualizar(_model));
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Empresa Parceira Sem Contatos",
            Description = "Teste Atualizar Empresa Parceira no Banco")]
        public void TesteAtualizarNumeroContatoNull()
        {
            _model.Contatos = null;

            Assert.ThrowsAsync<CustomException>(async () => await _service.Atualizar(_model));
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Empresa Parceira Mais de 3 Contatos",
            Description = "Teste Atualizar Empresa Parceira no Banco")]
        public void TesteAtualizarNumeroMaisContatosNull()
        {
            _model.Contatos = new List<ContatoEmpresaParceiraViewModel>
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
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Atualizar(_model));
        }
    }
}
