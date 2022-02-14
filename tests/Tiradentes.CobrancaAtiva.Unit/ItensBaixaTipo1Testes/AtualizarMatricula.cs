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
using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Unit.ItensBaixaTipo1Testes
{
    public class AtualizarMatricula
    {
        private CobrancaAtivaDbContext _context;
        private IItensBaixasTipo1Service _service;
        private ItensBaixaTipo1Model _model;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<CobrancaAtivaDbContext> optionsContext =
                new DbContextOptionsBuilder<CobrancaAtivaDbContext>()
                    .UseInMemoryDatabase("ItensBaixaTipo1Tests2")
                    .Options;

            _context = new CobrancaAtivaDbContext(optionsContext);
            IItensBaixasTipo1Repository repository = new ItensBaixasTipo1Repository(_context);
            _mapper = new Mapper(AutoMapperSetup.RegisterMappings());
            _service = new ItensBaixasTipo1Service(repository);

            _model = new ItensBaixaTipo1Model
            {
                Sequencia = 1,
                Multa = 10,
                Juros = 10,
                DataVencimento = DateTime.Now.AddDays(7),
                SituacaoAluno = "SituacaoAluno",
                Sistema = "Sistema",
                TipoInadimplencia = "TipoInadimplencia",
                Valor = 100
            };

            if(_context.ItensBaixaTipo1.CountAsync().Result == 0)
            {
                _context.ItensBaixaTipo1.Add(_model);
                _context.SaveChanges();
            }
            
            _context.ChangeTracker.Clear();
        }

        [Test]
        [TestCase(TestName = "Teste Atualizar Matricula Itens Baixa 1",
                   Description = "Teste Atualizar Matricula Itens Baixa 1 no Banco")]
        public async Task TesteAtualizarMatriculaValido()
        {
            await _service.AtualizarMatricula(DateTime.Now, 10, 10);

            Assert.Pass();
        }
    }
}
