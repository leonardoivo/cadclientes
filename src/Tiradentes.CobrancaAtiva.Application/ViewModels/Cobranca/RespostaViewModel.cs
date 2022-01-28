
using MongoDB.Bson;
using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca
{
    public class RespostaViewModel
    {
        public string MongoId { get; set; }
        public int TipoRegistro { get; set; }
        public string CPF { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public int Parcela { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public string ChaveInadimplencia { get; set; }
        public Int64 Matricula { get; set; }
        public int Periodo { get; set; } // itensGeração esta como Number(5)

        public decimal IdTitulo { get; set; }
        public int CodigoAtividade { get; set; }
        public int NumeroEvt { get; set; }
        public decimal IdPessoa { get; set; }
        public int CodigoBanco { get; set; }
        public int CodigoAgencia { get; set; }
        public int NumeroConta { get; set; }
        public Int64 NumeroCheque { get; set; }

        //tipo 1
        public decimal Juros { get; set; }
        public decimal Multa { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataFechamentoAcordo { get; set; }
        public int TotalParcelas { get; set; }

        //tipo 1 e tipo 2
        public DateTime DataVencimento { get; set; }
        public decimal ValorParcela { get; set; }

        //tipo 2
        public decimal SaldoDevedorTotal { get; set; }
        public string Produto { get; set; }
        public string DescricaoProduto { get; set; }
        public string Fase { get; set; }
        public string CodigoControleCliente { get; set; }

        public string PeriodoChequeDevolvido { get; set; }

        //tipo 3
        public string NossoNumero { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataBaixa { get; set; }
        public decimal ValorPago { get; set; }
        public string TipoPagamento { get; set; }

        public bool Integrado { get; set; }
    }
}
