﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ItensBaixaTipo1Model : BaseItensModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Sequencia { get; set; }
        public int? CodigoErro { get; set; }
        public decimal NumeroLinha { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public Int64 Matricula { get; set; }        
        public int Parcela { get; set; }
        public decimal Multa { get; set; }
        public decimal Juros { get; set; }
        public DateTime DataVencimento { get; set; }
        public string SituacaoAluno { get; set; }
        public string Sistema { get; set; }
        public string TipoInadimplencia { get; set; }
        public decimal Valor { get; set; }
    }
}
