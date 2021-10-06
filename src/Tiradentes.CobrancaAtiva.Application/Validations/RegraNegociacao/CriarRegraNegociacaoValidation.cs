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
            RuleFor(e => e.ModalidadeId)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

            RuleFor(e => e.SituacaoAlunoIds)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

            RuleFor(e => e.ValidadeInicial)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.TipoPagamentoIds)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentJurosMulta)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.PercentValor)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

             RuleFor(e => e.MesAnoInicial)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);
        }
    }
}
