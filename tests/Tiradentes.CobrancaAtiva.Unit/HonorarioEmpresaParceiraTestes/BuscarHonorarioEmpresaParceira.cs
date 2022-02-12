using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Unit.HonorarioEmpresaParceiraTestes
{
    public class BuscarHonorarioEmpresaParceira
    {
        private CobrancaAtivaDbContext _context;
        private IHonorarioEmpresaParceiraService _service;
        private HonorarioEmpresaParceiraModel _model;
        private IMapper _mapper;
        private CreateHonorarioEmpresaParceiraViewModel _criarViewModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("HonorarioEmpresaParceiraTests2")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            IHonorarioEmpresaParceiraRepository repository = new HonorarioEmpresaParceiraRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new HonorarioEmpresaParceiraService(repository, _mapper);

            _criarViewModel = new CreateHonorarioEmpresaParceiraViewModel
            {
                Id = 1,
                EmpresaParceiraId = 1,
                PercentualCobrancaIndevida = 0,
                ValorCobrancaIndevida = 0,
                FaixaEspecialVencidosMaiorQue  = 0,
                FaixaEspecialVencidosAte = 0,
                FaixaEspecialPercentualJuros = 0,
                FaixaEspecialPercentualMulta = 0,
                FaixaEspecialPercentualRecebimentoAluno = 0
            };

            if(_context.HonorarioEmpresaParceiras.CountAsync().Result == 0)
            {
                _model = _mapper.Map<HonorarioEmpresaParceiraModel>(_criarViewModel);
                _context.HonorarioEmpresaParceiras.Add(_model);
                _context.SaveChanges();
            }
            
            _context.ChangeTracker.Clear();
        }

        [Test]
        [TestCase(TestName = "Teste Buscar Honorario Empresa Parceira válido",
                   Description = "Teste Buscar Honorario Empresa Parceira no Banco")]
        public async Task TesteBuscarHonorarioEmpresaParceiraValido()
        {
            var queryParam = new ConsultaHonorarioEmpresaParceiraQueryParam()
            {
            };

            var Regras = await _service.Buscar(queryParam);

            Assert.AreEqual(1, Regras.TotalItems);
        }
    }
}
