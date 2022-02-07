using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;

namespace Tiradentes.CobrancaAtiva.Application.Validations.RegularizacaoParcelasAcordo
{
    public class RegularizacaoParcelasAcordoValidation : AbstractValidator<RegularizarParcelasAcordoViewModel>
    {
        public RegularizacaoParcelasAcordoValidation()
        {
            RuleFor(e => e.NumeroAcordo)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);

            RuleFor(e => e.Matricula)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(MensagensErroValidacao.CampoObrigatorio);
        }
    }
}
