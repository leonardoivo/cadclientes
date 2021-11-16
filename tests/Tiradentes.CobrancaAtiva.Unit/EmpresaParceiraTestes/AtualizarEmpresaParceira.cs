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
using Tiradentes.CobrancaAtiva.Application.Utils;
using System;

namespace Tiradentes.CobrancaAtiva.Unit.EmpresaParceiraTestes
{
    public class AtualizarEmpresaParceira
    {
        private EmpresaParceiraController _controller;
        private CobrancaAtivaDbContext _context;
        private IEmpresaParceiraService _service;
        private EmpresaParceiraViewModel _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests")
                    .Options;
            _context = new CobrancaAtivaDbContext(optionsContext);
            IEmpresaParceiraRepository repository = new EmpresaParceiraRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new EmpresaParceiraService(repository, _mapper);
            _controller = new EmpresaParceiraController(_service);

            _model = new EmpresaParceiraViewModel
            {
                Id = 1,
                NomeFantasia = "Nome Fantasia",
                RazaoSocial = "Razao Social",
                CNPJ = "97355899000105",
                NumeroContrato = "NumeroContrato",
                Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel> {
                    new ContatoEmpresaParceiraViewModel {
                        Id = 1,
                        Contato = "Teste",
                        Email = "teste@teste.com",
                        Telefone = "4444444444"
                    }
                }
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
        [TestCase(TestName = "Teste Atualizar Empresa Parceira",
                   Description = "Teste Atualizar Empresa Parceira no Banco")]
        public void TesteAtualizarEmpresaParceira()
        {
            _model.NomeFantasia = "Mudança";

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
            _model.Contatos = new System.Collections.Generic.List<ContatoEmpresaParceiraViewModel> {
                new ContatoEmpresaParceiraViewModel {
                    Contato = "Teste",
                    Email = "teste@teste.com",
                    Telefone = "4444444444"
                },
                new ContatoEmpresaParceiraViewModel {
                    Contato = "Teste",
                    Email = "teste@teste.com",
                    Telefone = "4444444444"
                },
                new ContatoEmpresaParceiraViewModel {
                    Contato = "Teste",
                    Email = "teste@teste.com",
                    Telefone = "4444444444"
                },
                new ContatoEmpresaParceiraViewModel {
                    Contato = "Teste",
                    Email = "teste@teste.com",
                    Telefone = "4444444444"
                }
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Atualizar(_model));
        }
    }
}
