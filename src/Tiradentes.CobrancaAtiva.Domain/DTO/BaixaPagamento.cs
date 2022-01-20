using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BaixaPagamento
    {
        public string NumeroAcordo { get; set; }
        public string EmpresaParceira { get; set; }
        public string InstituicaoEnsino { get; set; }
        public string Matricula { get; set; }
        public string ModalidadeEnsino { get; set; }
        public string DataNegociacao { get; set; }
        public string DataBaixa { get; set; }
        public int TotalParcelas { get; set; }
        public float ValorPago { get; set; }
        public float ValorMulta { get; set; }
        public float ValorJuros { get; set; }
        public float SaldoDevedor { get; set; }
        public string FormaPagamento { get; set; }
        public float Percentual { get; set; }
        public bool Politica { get; set; }

        public ICollection<BaixaPagamentoParcela> ParcelasAcordadas { get; set; }
        public ICollection<BaixaPagamentoParcela> ParcelasNegociadas { get; set; }
    }
}