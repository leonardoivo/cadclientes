using System;
using System.Net.Http;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Api.Controllers;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Microsoft.Extensions.Options;
using Moq;
using Tiradentes.CobrancaAtiva.Application.Configuration;

namespace Tiradentes.CobrancaAtiva.Unit.EmpresaParceiraTestes
{
    public class ExcluirEmpresaParceira
    {
        private EmpresaParceiraController _controller;
        private CobrancaAtivaDbContext _context;
        private IEmpresaParceiraService _service;
        private EmpresaParceiraViewModel _model;
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

            _model = new EmpresaParceiraViewModel
            {
                Id = 10,
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = "97355899000109",
                NumeroContrato = "NumeroContrato",
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel> {
                    new ContatoEmpresaParceiraViewModel {
                        Id = 10,
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
            };

            if(_context.EmpresaParceira.CountAsync().Result == 0)
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
        [TestCase(TestName = "Teste Excluir Empresa Parceira",
                   Description = "Teste Excluir Empresa Parceira no Banco")]
        public async Task TesteExcluirEmpresaParceira()
        {
            await _service.Deletar(_model.Id);

            Assert.Pass();
        }
    }
}
