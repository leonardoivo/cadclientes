using System;

namespace Tiradentes.CobrancaAtiva.Domain.Collections
{
    public class RespostasCollection
    {
        public string Id { get; set; }
        public DateTime DataResposta { get; set; } = DateTime.Now;

        public int TipoRegistro { get; set; }
        public int InstituicaoEnsino { get; set; }
        public int? Curso { get; set; }
        public Int64 CPF { get; set; }
        public string NomeAluno { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public int Parcela { get; set; }
        public Int64 CnpjEmpresaCobranca { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }

        public Int64 Matricula { get; set; }
        public string TipoInadimplencia { get; set; }
        public string ChaveInadimplencia { get; set; }
        public string Periodo { get; set; }



        public decimal IdTitulo { get; set; }
        public int CodigoAtividade { get; set; }
        public int NumeroEvt { get; set; }
        public decimal IdPessoa { get; set; }
        public int CodigoBanco { get; set; }
        public int CodigoAgencia { get; set; }
        public int NumeroConta { get; set; }
        public int NumeroCheque { get; set; }

        //public RespostaRegistroTipo1 RespostaRegistroTipo1 { get; set; }
        public decimal JurosParcela { get; set; }
        public decimal MultaParcela { get; set; }
        public decimal ValorTotalParcela { get; set; }
        public DateTime DataFechamentoAcordo { get; set; }
        public int TotalParcelasAcordo { get; set; }
        public DateTime DataVencimentoParcela { get; set; }
        public decimal ValorParcela { get; set; }


        //public RespostaRegistroTipo2 RespostaRegistroTipo2 { get; set; }
        //public DateTime DataVencimentoParcela { get; set; }
        //public decimal ValorParcela { get; set; }
        public decimal SaldoDevedorTotal { get; set; }
        public string Produto { get; set; }
        public string DescricaoProduto { get; set; }
        //public string Fase { get; set; }
        public string CodigoControleCliente { get; set; }


        //public RespostaRegistroTipo3 RespostaRegistroTipo3 { get; set; }
        public string NossoNumero { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataBaixa { get; set; }
        public decimal? ValorPago { get; set; }
        public string TipoPagamento { get; set; }


        public bool Integrado { get; set; } = false;

    }
}
