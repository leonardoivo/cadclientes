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
            RuleFor(e => e.EmpresaParceiraId)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);
        }
    }
}
