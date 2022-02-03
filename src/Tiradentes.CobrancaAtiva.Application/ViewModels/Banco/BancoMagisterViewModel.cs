using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Banco
{
    public class BancoMagisterViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Digito { get; set; }
        public string ContaContabil { get; set; }
    }
}