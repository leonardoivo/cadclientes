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
using System;

namespace Tiradentes.CobrancaAtiva.Unit.EmpresaParceiraTestes
{
    public class ExcluirEmpresaParceira
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
            _service = new EmpresaParceiraService(repository, null, _mapper);
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
        public async Task TesteExcluirEmpresaParceira()
        {
            await _service.Deletar(_model.Id);
        }
    }
}
