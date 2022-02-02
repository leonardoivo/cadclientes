﻿using System;

namespace Tiradentes.CobrancaAtiva.Domain.Collections
{
    public class RespostasCollection
    {
        public string Id { get; set; }
        public DateTime DataResposta { get; set; } = DateTime.Now;

        public int TipoRegistro { get; set; }
        public string InstituicaoEnsino { get; set; }
        public string Curso { get; set; }
        public string CPF { get; set; }
        public string NomeAluno { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public int Parcela { get; set; }
        public string CnpjEmpresaCobranca { get; set; }
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

        //tipo 1
        public decimal JurosParcela { get; set; }
        public decimal MultaParcela { get; set; }
        public decimal ValorTotalParcela { get; set; }
        public DateTime DataFechamentoAcordo { get; set; }
        public int TotalParcelasAcordo { get; set; }
        public DateTime DataVencimentoParcela { get; set; }
        public decimal ValorParcela { get; set; }

        //tipo 2
        public decimal SaldoDevedorTotal { get; set; }
        public string Produto { get; set; }
        public string DescricaoProduto { get; set; }
        //public string Fase { get; set; }
        public string CodigoControleCliente { get; set; }

        //tipo 3
        public string NossoNumero { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataBaixa { get; set; }
        public decimal ValorPago { get; set; }
        public string TipoPagamento { get; set; }


        public bool Integrado { get; set; } = false;

    }
}
