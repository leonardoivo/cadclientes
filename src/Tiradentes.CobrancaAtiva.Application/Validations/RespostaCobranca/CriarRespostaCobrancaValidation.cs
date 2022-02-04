using FluentValidation;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;

namespace Tiradentes.CobrancaAtiva.Application.Validations.RespostaCobranca
{
    public class CriarRespostaCobrancaValidation : AbstractValidator<RespostaViewModel>
    {
        public CriarRespostaCobrancaValidation()
        {
            RuleFor(e => e.CPF.ToString())
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .Length(11).WithMessage("CPF inválido")
                .Matches(@"^[\d]+$").WithMessage("CPF inválido");
        }
    }
}
