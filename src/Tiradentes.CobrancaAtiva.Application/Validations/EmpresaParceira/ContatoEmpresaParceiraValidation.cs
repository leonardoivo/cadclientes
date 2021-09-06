using FluentValidation;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Application.Validations.EmpresaParceira
{
    public class ContatoEmpresaParceiraValidation : AbstractValidator<ContatoEmpresaParceiraViewModel>
    {
        public ContatoEmpresaParceiraValidation()
        {
            RuleFor(e => e.Contato)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .MaximumLength(50).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .MaximumLength(50).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.Telefone)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .MaximumLength(12).WithMessage(MensagensErroValidacao.TamanhaMaximo);
        }
    }
}
