using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento
{
    public class ItensBaixaPagamentoViewModel
    {
        public int Tipo { get; set; } 
        public long? NumeroLinha { get; set; }
        public long Matricula { get; set; }
        public string NomeAluno { get; set; }
        public long NumeroAcordo { get; set; }
        public int Parcela { get; set; }
        public DateTime? DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public decimal? Multa { get; set; }
        public decimal? Juros { get; set; }
        public int? Erro { get; set; }
    }
}