using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca
{
    public class RegularizarParcelasAcordoViewModel
    {
        public Int64 CnpjEmpresaCobranca { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Parcela { get; set; }
        public string Texto { get; set; }

        public Int64 Matricula { get; set; }
        public string CPF { get; set; }
        public string Periodo { get; set; }
        public string Sistema { get; set; }
        public decimal? IdTitulo { get; set; }
        public int? CodigoAtividade { get; set; }
        public int NumeroEvt { get; set; }

        public decimal? IdPessoa { get; set; }
        public int CodigoBanco { get; set; }
        public int CodigoAgencia { get; set; }
        public int NumeroConta { get; set; }
        public decimal NumeroCheque { get; set; }
        

        public string TipoInadimplencia { get; set; }

        public decimal ObterPeriodo()
        {
            if (this.TipoInadimplencia.Equals("C") || this.TipoInadimplencia.Equals("X"))
                return 1;

            return Convert.ToDecimal(Periodo);
        }
        public string ObterPeriodoOutros()
        {
            if (this.TipoInadimplencia.Equals("C") || this.TipoInadimplencia.Equals("X"))
                return Periodo;

            return "1";
        }
    }
}
