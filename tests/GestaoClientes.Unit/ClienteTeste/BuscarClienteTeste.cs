
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using GestaoClientes.Application.AutoMapper;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Domain.Models;
using GestaoClientes.Infrastructure.Context;
using GestaoClientes.Infrastructure.Repositories;
using GestaoClientes.Services.Interfaces;
using GestaoClientes.Services.Services;
using System.Threading.Tasks;
using Moq;

namespace GestaoClientes.Unit.CursoTestes
{
    public class BuscarClienteTeste
    {
        private GestaoClientesDbContext _context;
        private IClienteService _cliente;
        private Mock<IClienteRepository> _clienteRepository;
        private IMapper _mapper;
        private ClienteModel _model;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<GestaoClientesDbContext> optionsContext =
                new DbContextOptionsBuilder<GestaoClientesDbContext>()
                    .UseInMemoryDatabase("BuscaCliente")
                    .Options;
            _context = new GestaoClientesDbContext(optionsContext);
            _clienteRepository = new Mock<IClienteRepository>();

            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _cliente = new ClienteService(_clienteRepository.Object, _mapper);

            _model = new ClienteModel
            {
                 IdCliente=1,
                 Nome="teste",
                 Porte = Domain.Enums.Porte.Pequeno
            };

            if (_context.Cliente.CountAsync().Result == 0)
            {
                _context.Cliente.Add(_model);
                _context.SaveChanges();
            }

            _context.ChangeTracker.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            //_service?.Dispose();
        }

        [Test]
        [TestCase(TestName = "Teste Buscar cliente cadastrado",
                   Description = "Teste Buscar cliente cadastrado")]
        public  void TesteBuscarPorInstituicaoModalidadeInvalido()
        {
            var Curso =  _cliente.ObterClienteId(_model.IdCliente);

            Assert.AreEqual(0, Curso.Count);
        }
    }
}
