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
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentValorAVista)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentJurosMultaCartao)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentValorCartao)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.QuantidadeParcelasCartao)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentJurosMultaBoleto)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentValorBoleto)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentEntradaBoleto)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.QuantidadeParcelasBoleto)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.InadimplenciaInicial)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);
        }
    }
}
