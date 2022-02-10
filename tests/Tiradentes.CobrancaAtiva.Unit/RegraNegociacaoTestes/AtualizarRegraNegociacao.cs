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
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using System;
using Microsoft.Extensions.Options;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Unit.Fakes;
using Tiradentes.CobrancaAtiva.Application.Utils;

namespace Tiradentes.CobrancaAtiva.Unit.RegraNegociacaoTestes
{
    public class AtualizarRegraNegociacao
    {
        private RegraNegociacaoController _controller;
        private CobrancaAtivaDbContext _context;
        private IRegraNegociacaoService _service;
        private RegraNegociacaoModel _model;
        private IMapper _mapper;
        private IOptions<EncryptationConfig> _encryptationConfig;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("CobrancaAtivaTests")
                    .Options;
            _encryptationConfig = Options.Create<EncryptationConfig>(new EncryptationConfig()
            {
                BaseUrl = "https://encrypt-service-2kcoisahga-ue.a.run.app/",
                DecryptAuthorization = "bWVjLWVuYzpwYXNzd29yZA==",
                EncryptAuthorization = "bWVjLWRlYzpwYXNzd29yZA=="
            });
            _context = new CobrancaAtivaDbContext(optionsContext);
            IRegraNegociacaoRepository repository = new RegraNegociacaoRepository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new RegraNegociacaoService(repository, _mapper);
            _controller = new RegraNegociacaoController(_service);

            var alterarViewModel = new AlterarRegraNegociacaoViewModel
            {
                InstituicaoId = 1,
                ModalidadeId = 1,
                PercentJurosMultaAVista = 0,
                PercentValorAVista = 0,
                PercentJurosMultaCartao  = 0,
                PercentValorCartao = 0,
                QuantidadeParcelasCartao = 0,
                PercentJurosMultaBoleto = 0,
                PercentValorBoleto = 0,
                PercentEntradaBoleto = 0,
                QuantidadeParcelasBoleto = 0,
                Status = true,
                InadimplenciaInicial = DateTime.Now,
                InadimplenciaFinal = DateTime.Now,
                ValidadeInicial = DateTime.Now,
                ValidadeFinal = DateTime.Now,
                CursoIds = new int[1]{ 1 },
                SituacaoAlunoIds = new int[1]{ 1 },
                TitulosAvulsosId = new int[1]{ 1 },
                TipoTituloIds = new int[1]{ 1 },
            };

            if(_context.RegraNegociacao.CountAsync().Result == 0)
            {
                _model = _mapper.Map<RegraNegociacaoModel>(alterarViewModel);
                _context.RegraNegociacao.Add(_model);
                _context.SaveChanges();
            }
            
            _context.ChangeTracker.Clear();
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Regra Negociacao",
                   Description = "Teste Atualizar Regra Negociacao no Banco")]
        public async Task TesteAtualizarRegraNegociacaoValido()
        {
            var alterarViewModel = new AlterarRegraNegociacaoViewModel
            {
                Id = _model.Id,
                InstituicaoId = 1,
                ModalidadeId = 1,
                PercentJurosMultaAVista = 0,
                PercentValorAVista = 0,
                PercentJurosMultaCartao  = 0,
                PercentValorCartao = 0,
                QuantidadeParcelasCartao = 0,
                PercentJurosMultaBoleto = 0,
                PercentValorBoleto = 0,
                PercentEntradaBoleto = 0,
                QuantidadeParcelasBoleto = 0,
                Status = true,
                InadimplenciaInicial = DateTime.Now,
                InadimplenciaFinal = DateTime.Now,
                ValidadeInicial = DateTime.Now,
                ValidadeFinal = DateTime.Now,
                CursoIds = new int[1]{ 1 },
                SituacaoAlunoIds = new int[1] { 1 },
                TitulosAvulsosId = new int[1]{ 1 },
                TipoTituloIds = new int[1]{ 1 },
            };

            await _service.Alterar(alterarViewModel);
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Regra Negociacao não encontrada",
                   Description = "Teste Atualizar Regra Negociacao não encontrada no Banco")]
        public void TesteAtualizarRegraNegociacaoInvalido()
        {
            var alterarViewModel = new AlterarRegraNegociacaoViewModel
            {
                Id = 123,
                InstituicaoId = 1,
                ModalidadeId = 1,
                PercentJurosMultaAVista = 0,
                PercentValorAVista = 0,
                PercentJurosMultaCartao  = 0,
                PercentValorCartao = 0,
                QuantidadeParcelasCartao = 0,
                PercentJurosMultaBoleto = 0,
                PercentValorBoleto = 0,
                PercentEntradaBoleto = 0,
                QuantidadeParcelasBoleto = 0,
                Status = true,
                InadimplenciaInicial = DateTime.Now,
                InadimplenciaFinal = DateTime.Now,
                ValidadeInicial = DateTime.Now,
                ValidadeFinal = DateTime.Now,
                CursoIds = new int[1]{ 1 },
                SituacaoAlunoIds = new int[1] { 1 },
                TitulosAvulsosId = new int[1]{ 1 },
                TipoTituloIds = new int[1]{ 1 },
            };

            Assert.ThrowsAsync<CustomException>(async () => await _service.Alterar(alterarViewModel));
        }
    }
}
