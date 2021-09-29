using FluentValidation;
using System.Linq;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Application.Validations.HonorarioEmpresaParceira
{
    public class CriarHonorarioEmpresaParceiraValidation : AbstractValidator<CreateHonorarioEmpresaParceiraViewModel>
    {
        public CriarHonorarioEmpresaParceiraValidation()
        {
            RuleFor(e => e.ModalidadeId)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

            RuleFor(e => e.InstituicaoId)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

            RuleFor(e => e.EmpresaParceiraId)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

            RuleFor(e => e.PercentualNegociacaoEmpresaParceira)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .When(e => e.ValorNegociacaoEmpresaParceira == 0);

            RuleFor(e => e.ValorNegociacaoEmpresaParceira)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .When(e => e.PercentualNegociacaoEmpresaParceira == 0);

            RuleFor(e => e.ValorNegociacaoInstituicaoEnsino)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .When(e => e.PercentualNegociacaoInstituicaoEnsino == 0);

            RuleFor(e => e.PercentualNegociacaoInstituicaoEnsino)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio)
                .When(e => e.ValorNegociacaoInstituicaoEnsino == 0);
        }
    }
}
