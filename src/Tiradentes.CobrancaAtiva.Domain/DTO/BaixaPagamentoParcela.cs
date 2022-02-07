using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BaixaPagamentoParcela
    {
        public string Periodo { get; set; }
        public int Parcela { get; set; }
        public string DataVencimento { get; set; }
        public string DataBaixa { get; set; }
        public string DataPagamento { get; set; }
        public float Valor { get; set; }
        public float ValorPago { get; set; }
        public string TipoPagamento { get; set; }

        public int Banco { get; set; }
        public int Agencia { get; set; }
        public long Cheque { get; set; }

        public long AcordoOriginal { get; set; }

        public decimal ValorDebitoOriginal { get; set; }

        public decimal NumeroAcordo { get; set; }
        public decimal Matricula { get; set; }
        public string Sistema { get; set; }
        public decimal? IdTitulo { get; set; }
        public int? CodigoAtividade { get; set; }
        public int? NumeroEvt { get; set; }
        public decimal? IdPessoa { get; set; }

        public int NumeroConta { get; set; }

        public string CpfCnpj { get; set; }
        public string TipoInadimplencia { get; set; }

    }
}