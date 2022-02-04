using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento
{
    public class BaixaPagamentoParcelaManualViewModel
    {
        public decimal NumeroAcordo { get; set; }        
        public decimal Parcela { get; set; }
        public DateTime DataBaixa { get; set; }
        public decimal ValorBaixa { get; set; }
        public decimal Matricula { get; set; }
        public int CodigoBanco { get; set; }
        public string Motivo { get; set; }
    }
}