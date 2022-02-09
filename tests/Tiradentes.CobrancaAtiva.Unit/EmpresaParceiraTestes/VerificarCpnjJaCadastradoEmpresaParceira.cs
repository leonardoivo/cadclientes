using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Api.Controllers;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;

namespace Tiradentes.CobrancaAtiva.Unit.EmpresaParceiraTestes
{
    public class VerificarCpnjJaCadastradoEmpresaParceira
    {
        private EmpresaParceiraController _controller;
        private CobrancaAtivaDbContext _context;
        private IEmpresaParceiraService _service;
        private EmpresaParceiraViewModel _model;
        private IMapper _mapper;
        private IOptions<EncryptationConfig> _encryptationConfig;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("VerificarCpnjJaCadastradoEmpresaParceiraTests")
                    .Options;
            _encryptationConfig = Options.Create<EncryptationConfig>(new EncryptationConfig()
            {
                BaseUrl = "https://encrypt-service-2kcoisahga-ue.a.run.app/",
                DecryptAuthorization = "bWVjLWVuYzpwYXNzd29yZA==",
                EncryptAuthorization = "bWVjLWRlYzpwYXNzd29yZA=="
            });
            _context = new CobrancaAtivaDbContext(optionsContext);
            IEmpresaParceiraRepository repository = new EmpresaParceiraRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new EmpresaParceiraService(repository, _mapper, _encryptationConfig);
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
                Contatos = new List<ContatoEmpresaParceiraViewModel> {
                    new ContatoEmpresaParceiraViewModel {
                        Id = 1,
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                },
                ChaveIntegracaoSap = "123423525"
            };

            if(_context.EmpresaParceira.CountAsync().Result == 0)
            {
                _context.EmpresaParceira.Add(_mapper.Map<EmpresaParceiraModel>(_model));
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
        [TestCase(TestName = "Teste Verificar Cnpj",
                   Description = "Teste Verificar Cnpj no Banco")]
        public async Task VerificarCnpjJaCadastrado()
        {
            await _service.VerificarCnpjJaCadastrado("28.992.700/0001-29", 1);

            Assert.Pass();
        }
    }
}
