
using MongoDB.Bson;
using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca
{
    public class RespostaViewModel
    {
        public string MongoId { get; set; }

        public int TipoRegistro { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public string ChaveInadimplencia { get; set; }
        public string CPF { get; set; }
        public Int64 Matricula { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public int Parcela { get; set; }



        /*
         * O campo é referente ao periodo e ao periodo_cheque_devolvido
         * deve-se ser analisado o periodo para atividades de extensão
         * no CSV enviamos string e receberemos na resposta como string, tratar para distribuir
         * a informação entre  periodo e periodo_cheque_devolvido conforme tipo de inadimplencia
         */
        
        public decimal Periodo { get; set; } // itensGeração esta como Number(5)
        public decimal IdTitulo { get; set; }
        public int CodigoAtividade { get; set; }
        public int NumeroEvt { get; set; }
        public decimal IdPessoa { get; set; }
        public int CodigoBanco { get; set; }
        public int CodigoAgencia { get; set; }
        public int NumeroConta { get; set; }
        public Int64 NumeroCheque { get; set; }


        //public RespostaRegistroTipo1ViewModel RespostaRegistroTipo1 { get; set; }
        //public RespostaRegistroTipo2ViewModel RespostaRegistroTipo2 { get; set; }
        //public RespostaRegistroTipo3ViewModel RespostaRegistroTipo3 { get; set; }

        public decimal JurosParcela { get; set; }
        public decimal MultaParcela { get; set; }
        public decimal ValorTotalParcela { get; set; }
        public DateTime DataFechamentoAcordo { get; set; }
        public int TotalParcelasAcordo { get; set; }
        public DateTime DataVencimentoParcela { get; set; }
        public decimal ValorParcela { get; set; }

        //public DateTime DataVencimentoParcela { get; set; }
        //public decimal ValorParcela { get; set; }
        public decimal SaldoDevedorTotal { get; set; }
        public string Produto { get; set; }
        public string DescricaoProduto { get; set; }
        public string Fase { get; set; }
        public string CodigoControleCliente { get; set; }

        public string NossoNumero { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataBaixa { get; set; }
        public decimal ValorPago { get; set; }
        public string TipoPagamento { get; set; }

        public bool Integrado { get; set; }
    }
}
