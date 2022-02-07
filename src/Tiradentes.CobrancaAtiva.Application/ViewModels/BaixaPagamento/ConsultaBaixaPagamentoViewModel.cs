using System;
using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento
{
    public class ConsultaBaixaPagamentoViewModel
    {
        public DateTime DataBaixa { get; set; }
        public string NomeEmpresaParceira { get; set; }
        public string NomeInstituicaoEnsino { get; set; }
        public int QuantidadeTipo1 { get; set; }
        public int QuantidadeTipo2 { get; set; }
        public int QuantidadeTipo3 { get; set; }
        public decimal ValorTipo1 { get; set; }
        public decimal ValorTipo2 { get; set; }
        public decimal ValorTipo3 { get; set; }
        public int QuantidadeErros { get; set; }
        public IEnumerable<ItensBaixaPagamentoViewModel> Items { get; set; }
    }
}