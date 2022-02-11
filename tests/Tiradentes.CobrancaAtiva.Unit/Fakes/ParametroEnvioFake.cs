using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Application.ViewModels.TituloAvulso;

namespace Tiradentes.CobrancaAtiva.Unit.Fakes
{
    public class ParametroEnvioFake
    {
        public static Faker<BuscaParametroEnvioViewModel> GerarBuscarParametroEnvio { get; } =
        new Faker<BuscaParametroEnvioViewModel>("pt_BR")
        .RuleFor(cc => cc.Id, 123)
        .RuleFor(cc => cc.EmpresaParceira, new EmpresaParceiraViewModel())
        .RuleFor(cc => cc.DiaEnvio, 123)
        .RuleFor(cc => cc.Status, true)
        .RuleFor(cc => cc.MotivoInativacao, "ABC")
        .RuleFor(cc => cc.InadimplenciaInicial, DateTime.Now)
        .RuleFor(cc => cc.InadimplenciaFinal, DateTime.Now)
        .RuleFor(cc => cc.ValidadeInicial, DateTime.Now)
        .RuleFor(cc => cc.ValidadeFinal, DateTime.Now)
        .RuleFor(cc => cc.Instituicao, new InstituicaoViewModel())
        .RuleFor(cc => cc.Modalidade, new ModalidadeViewModel());
    }
}