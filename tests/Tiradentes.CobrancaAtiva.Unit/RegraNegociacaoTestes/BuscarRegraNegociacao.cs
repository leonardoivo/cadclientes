﻿// using AutoMapper;
// using Microsoft.EntityFrameworkCore;
// using NUnit.Framework;
// using Tiradentes.CobrancaAtiva.Application.AutoMapper;
// using Tiradentes.CobrancaAtiva.Domain.Interfaces;
// using Tiradentes.CobrancaAtiva.Domain.Models;
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
//     public class BuscarRegraNegociacao
//     {
//         private CobrancaAtivaDbContext _context;
//         private IRegraNegociacaoService _service;
//         private RegraNegociacaoModel _model;
//         private IMapper _mapper;
//         private CriarRegraNegociacaoViewModel _criarViewModel;

//         [SetUp]
//         public void Setup()
//         {
//             DbContextOptions<CobrancaAtivaDbContext> optionsContext =
//                 new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
//                     .UseInMemoryDatabase("CobrancaAtivaTests3")
//                     .Options;

//             _context = new CobrancaAtivaDbContext(optionsContext);
//             IRegraNegociacaoRepository repository = new RegraNegociacaoRepository(_context);
//             _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
//             _service = new RegraNegociacaoService(repository, _mapper);

//             _criarViewModel = new CriarRegraNegociacaoViewModel
//             {
//                 InstituicaoId = 1,
//                 ModalidadeId = 1,
//                 PercentJurosMultaAVista = 0,
//                 PercentValorAVista = 0,
//                 PercentJurosMultaCartao  = 0,
//                 PercentValorCartao = 0,
//                 QuantidadeParcelasCartao = 0,
//                 PercentJurosMultaBoleto = 0,
//                 PercentValorBoleto = 0,
//                 PercentEntradaBoleto = 0,
//                 QuantidadeParcelasBoleto = 0,
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

//             if(_context.RegraNegociacao.CountAsync().Result == 0)
//             {
//                 _model = _mapper.Map<RegraNegociacaoModel>(_criarViewModel);
//                 _context.RegraNegociacao.Add(_model);
//                 _context.SaveChanges();

//                 _criarViewModel.Status = false;

//                 _context.RegraNegociacao.Add(_mapper.Map<RegraNegociacaoModel>(_criarViewModel));
//                 _context.SaveChanges();
//             }
            
//             _context.ChangeTracker.Clear();
//         }

//         [Test]
//         [TestCase(TestName = "Teste Buscar Regra Negociacao válido",
//                    Description = "Teste Buscar Regra Negociacao no Banco")]
//         public async Task TesteBuscarRegraNegociacaoValido()
//         {
//             var queryParam = new ConsultaRegraNegociacaoQueryParam()
//             {
//                 Status = true,
//             };

//             var Regras = await _service.Buscar(queryParam);

//             Assert.AreEqual(1, Regras.TotalItems);
//         }

//         [Test]
//         [TestCase(TestName = "Teste Buscar Regra Negociacao inválido",
//                    Description = "Teste Buscar Regra Negociacao no Banco")]
//         public async Task TesteBuscarRegraNegociacaoInvalido()
//         {
//             var queryParam = new ConsultaRegraNegociacaoQueryParam()
//             {
//                 InstituicaoId = 123123,
//                 ModalidadeId = 112311,
//                 Status = true,
//                 ValidadeInicial = DateTime.Now.AddDays(-7),
//                 ValidadeFinal = DateTime.Now.AddDays(7),
//                 InadimplenciaInicial = DateTime.Now.AddDays(-7),
//                 InadimplenciaFinal = DateTime.Now.AddDays(7),
//             };

//             var Regras = await _service.Buscar(queryParam);

//             Assert.AreEqual(0, Regras.TotalItems);
//         }
//     }
// }