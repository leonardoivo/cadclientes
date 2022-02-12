using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
using Tiradentes.CobrancaAtiva.Services.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Services;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Unit.HonorarioEmpresaParceiraTestes
{
    public class CriarHonorarioEmpresaParceira
    {
        private CobrancaAtivaDbContext _context;
        private IHonorarioEmpresaParceiraService _service;
        private IMapper _mapper;
        private CreateHonorarioEmpresaParceiraViewModel _criarViewModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("HonorarioEmpresaParceira3")
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
        }

        [Test]
        [TestCase(TestName = "Teste Criar Honorario Empresa Parceira válido",
                   Description = "Teste Criar Honorario Empresa Parceira no Banco")]
        public async Task TesteCriarHonorarioEmpresaParceiraValido()
        {
            await _service.Criar(_criarViewModel);

            var Regras = await _service.Buscar(new ConsultaHonorarioEmpresaParceiraQueryParam());

            Assert.AreEqual(1, Regras.TotalItems);
        }
    }
}
