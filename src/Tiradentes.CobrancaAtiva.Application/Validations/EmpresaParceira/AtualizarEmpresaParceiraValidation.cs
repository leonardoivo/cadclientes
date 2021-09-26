using FluentValidation;
using System.Linq;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Application.Validations.EmpresaParceira
{
    public class AtualizarEmpresaParceiraValidation : AbstractValidator<EmpresaParceiraViewModel>
    {
        public AtualizarEmpresaParceiraValidation()
        {
            RuleFor(e => e.NomeFantasia)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .MaximumLength(100).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.RazaoSocial)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .MaximumLength(100).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.Sigla)
                .MaximumLength(100).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.CNPJ)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .Must(cnpj => CommonValidacoes.ValidarCnpj(cnpj)).WithMessage("CNPJ inválido");

            RuleFor(e => e.NumeroContrato)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .MaximumLength(40).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.AditivoContrato)
                .MaximumLength(40).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.URL)
               .MaximumLength(100).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.CEP)
               .Length(8).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.Estado)
               .MaximumLength(50).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.Cidade)
               .MaximumLength(40).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.Logradouro)
               .MaximumLength(70).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.Numero)
               .MaximumLength(10).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.Complemento)
               .MaximumLength(40).WithMessage(MensagensErroValidacao.TamanhaMaximo);

            RuleFor(e => e.Contatos)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .Must(contatos => contatos.Any()).WithMessage("Um Contato é obrigatório")
                .Must(contatos => contatos.Count <= 3).WithMessage("No máximo 3 contatos")
                .ForEach(contatos => contatos.SetValidator(new ContatoEmpresaParceiraValidation()));
        }
    }
}
