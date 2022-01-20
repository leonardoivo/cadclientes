using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BaixaPagamentoParcela
    {
        public string Periodo { get; set; }
        public int Parcela { get; set; }
        public string DataVencimento { get; set ;}
        public string DataBaixa { get; set; }
        public string DataPagamento { get; set; }
        public float Valor { get; set; }
        public float ValorPago { get; set; }
        public string TipoPagamento { get; set; }

        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Cheque { get; set; }

        public string AcordoOriginal { get; set; }
    }
}