
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using GestaoClientes.Application.AutoMapper;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Infrastructure.Context;
using GestaoClientes.Services.Interfaces;
using GestaoClientes.Services.Services;
using Moq;
using GestaoClientes.Infrastructure.Repositories;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using GestaoClientes.Application.ViewModels.Cliente;
using GestaoClientes.Domain.Enums;

namespace GestaoClientes.Unit.CobrancaTestes
{
    public class CriarClienteTeste
    {
        private GestaoClientesDbContext _context;
        private IClienteService _cliente;
        private Mock<IClienteRepository> _clienteRepository;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<GestaoClientesDbContext> optionsContext =
                new DbContextOptionsBuilder<GestaoClientesDbContext>()
                    .UseInMemoryDatabase("ClienteTeste2")
                    .Options;
            _context = new GestaoClientesDbContext(optionsContext);

            _clienteRepository = new Mock<IClienteRepository>();

            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());

            _cliente = new ClienteService(_clienteRepository.Object, _mapper);
        }

        [TearDown]
        public void TearDown()
        {
           // _cliente?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Cadastrar Cliente",
                   Description = "Teste Cadastrar Cliente")]
        public void  TesteCriarValido()
        {
            var viewModel = new ClienteViewModel()
            {
                Nome = "TEste",
               Porte = Porte.Pequeno
            };

             _cliente.InserirCliente(viewModel);

//Assert.AreEqual(viewModel.CnpjEmpresaCobranca, result.CnpjEmpresaCobranca);
        }
    }
}
