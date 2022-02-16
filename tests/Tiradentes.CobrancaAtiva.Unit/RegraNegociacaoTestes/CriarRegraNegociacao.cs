// using AutoMapper;
// using Microsoft.EntityFrameworkCore;
// using NUnit.Framework;
// using Tiradentes.CobrancaAtiva.Application.AutoMapper;
// using Tiradentes.CobrancaAtiva.Domain.Interfaces;
// using Tiradentes.CobrancaAtiva.Infrastructure.Context;
// using Tiradentes.CobrancaAtiva.Infrastructure.Repositories;
// using Tiradentes.CobrancaAtiva.Services.Interfaces;
// using Tiradentes.CobrancaAtiva.Services.Services;
// using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
// using System;
// using System.Threading.Tasks;
// using Tiradentes.CobrancaAtiva.Application.QueryParams;

// namespace Tiradentes.CobrancaAtiva.Unit.RegraNegociacaoTestes
// {
//     public class CriarRegraNegociacao
//     {
//         private CobrancaAtivaDbContext _context;
//         private IRegraNegociacaoService _service;
//         private IMapper _mapper;
//         private CriarRegraNegociacaoViewModel _criarViewModel;

//         [SetUp]
//         public void Setup()
//         {
//             DbContextOptions<CobrancaAtivaDbContext> optionsContext =
//                 new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
//                     .UseInMemoryDatabase("CobrancaAtivaTests2")
//                     .Options;

//             _context = new CobrancaAtivaDbContext(optionsContext);
//             IRegraNegociacaoRepository repository = new RegraNegociacaoRepository(_context);
//             _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
//             _service = new RegraNegociacaoService(repository, _mapper);

//             _criarViewModel = new CriarRegraNegociacaoViewModel
//             {
//                 InstituicaoId = 1,
//                 ModalidadeId = 1,
//                 PercentJurosMultaAVista = 1,
//                 PercentValorAVista = 1,
//                 PercentJurosMultaCartao  = 1,
//                 PercentValorCartao = 1,
//                 QuantidadeParcelasCartao = 1,
//                 PercentJurosMultaBoleto = 1,
//                 PercentValorBoleto = 1,
//                 PercentEntradaBoleto = 1,
//                 QuantidadeParcelasBoleto = 1,
//                 Status = true,
//                 InadimplenciaInicial = DateTime.Now,
//                 InadimplenciaFinal = DateTime.Now,
//                 ValidadeInicial = DateTime.Now,
//                 ValidadeFinal = DateTime.Now,
//                 CursoIds = new int[1]{ 1 },
//                 SituacaoAlunoIds = new int[1]{ 1 },
//                 TitulosAvulsosId = new int[1]{ 1 },
//                 TipoTituloIds = new int[1]{ 1 },
//             };
//         }

//         [Test]
//         [TestCase(TestName = "Teste Criar Regra Negociacao válido",
//                    Description = "Teste Criar Regra Negociacao no Banco")]
//         public async Task TesteCriarRegraNegociacaoValido()
//         {
//             await _service.Criar(_criarViewModel);

//             var Regras = await _service.Buscar(new ConsultaRegraNegociacaoQueryParam());

//             Assert.AreEqual(1, Regras.TotalItems);
//         }

//         [Test]
//         [TestCase(TestName = "Teste Verificar Criar Regra Conflitante Negociacao",
//                    Description = "Teste Verificar Criar Regra Conflitante Negociacao no Banco")]
//         public async Task TesteVerificarConflitanteRegraNegociacaoValido()
//         {
//             var criarViewModel = _criarViewModel;

//             var conflito = await _service.VerificarRegraConflitante(_criarViewModel);

//             Assert.AreEqual(null, conflito);
//         }
//     }
// }
