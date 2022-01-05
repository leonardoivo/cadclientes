using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Domain.Collections
{
    public class RespostasCollection
    {
        public DateTime DataResposta { get; set; } = DateTime.Now;

        public string TipoRegistro { get; set; }
        public string CPF { get; set; }
        public string NumeroAcordo { get; set; }
        public string Parcela { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public string ChaveInadimplencia { get; set; }

        //tipo 1
        public string Juros { get; set; }
        public string Multa { get; set; }
        public string ValorTotal { get; set; }
        public string DataFechamentoAcordo { get; set; }
        public string TotalParcelas { get; set; }

        //tipo 1 e tipo 2
        public string DataVencimento { get; set; }
        public string ValorParcela { get; set; }

        //tipo 2
        public string SaldoDevedorTotal { get; set; }
        public string Produto { get; set; }
        public string DescricaoProduto { get; set; }
        public string Fase { get; set; }
        public string CodigoControleCliente { get; set; }

        //tipo 3
        public string NossoNumero { get; set; }
        public string DataPagamento { get; set; }
        public string DataBaixa { get; set; }
        public string ValorPago { get; set; }
        public string TipoPagamento { get; set; }

        public bool Integrado { get; set; } = false;

    }
}
