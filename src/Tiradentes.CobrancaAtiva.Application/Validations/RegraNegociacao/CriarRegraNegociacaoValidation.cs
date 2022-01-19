using FluentValidation;
using System.Linq;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;

namespace Tiradentes.CobrancaAtiva.Application.Validations.HonorarioEmpresaParceira
{
    public class CriarRegraNegociacaoValidation : AbstractValidator<CriarRegraNegociacaoViewModel>
    {
        public CriarRegraNegociacaoValidation()
        {
            RuleFor(e => e.ValidadeInicial)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentJurosMultaAVista)
                .NotNull().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentValorAVista)
                .NotNull().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentJurosMultaCartao)
                .NotNull().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentValorCartao)
                .NotNull().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.QuantidadeParcelasCartao)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentJurosMultaBoleto)
                .NotNull().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentValorBoleto)
                .NotNull().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentEntradaBoleto)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.QuantidadeParcelasBoleto)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.InadimplenciaInicial)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);
        }
    }
}
