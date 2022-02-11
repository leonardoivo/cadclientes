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
using System;
using Microsoft.Extensions.Options;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Utils;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Unit.HonorarioEmpresaParceiraTestes
{
    public class AtualizarHonorarioEmpresaParceira
    {
        private HonorarioEmpresaParceiraController _controller;
        private CobrancaAtivaDbContext _context;
        private IHonorarioEmpresaParceiraService _service;
        private HonorarioEmpresaParceiraModel _model;
        private IMapper _mapper;
        private IOptions<EncryptationConfig> _encryptationConfig;
        private CreateHonorarioEmpresaParceiraViewModel _alterarViewModel;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("HonorarioEmpresaParceiraTests")
                    .Options;
            _encryptationConfig = Options.Create<EncryptationConfig>(new EncryptationConfig()
            {
                BaseUrl = "https://encrypt-service-2kcoisahga-ue.a.run.app/",
                DecryptAuthorization = "bWVjLWVuYzpwYXNzd29yZA==",
                EncryptAuthorization = "bWVjLWRlYzpwYXNzd29yZA=="
            });
            _context = new CobrancaAtivaDbContext(optionsContext);
            IHonorarioEmpresaParceiraRepository repository = new HonorarioEmpresaParceiraRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new HonorarioEmpresaParceiraService(repository, _mapper);
            _controller = new HonorarioEmpresaParceiraController(_service);

            _alterarViewModel = new CreateHonorarioEmpresaParceiraViewModel
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
                _model = _mapper.Map<HonorarioEmpresaParceiraModel>(_alterarViewModel);
                _context.HonorarioEmpresaParceiras.Add(_model);
                _context.SaveChanges();
            }
            
            _context.ChangeTracker.Clear();
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Honorario Empresa Parceira Negociacao",
                   Description = "Teste Atualizar Honorario Empresa Parceira Negociacao no Banco")]
        public async Task TesteAtualizarHonorarioEmpresaParceiraValido()
        {
            var alterarViewModel = _alterarViewModel;

            alterarViewModel.Id = _model.Id;
            
            alterarViewModel.ValorCobrancaIndevida = 1;

            var result = await _service.Atualizar(alterarViewModel);

            Assert.AreEqual(1, result.ValorCobrancaIndevida);
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Honorario Empresa Parceira não encontrada",
                   Description = "Teste Atualizar Honorario Empresa Parceira não encontrada no Banco")]
        public void TesteAtualizarHonorarioEmpresaParceiraInvalido()
        {
            var alterarViewModel = _alterarViewModel;
           
            alterarViewModel.Id = 123;

            Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _service.Atualizar(alterarViewModel));
        }
    }
}
