﻿using System;

namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class RegraNegociacaoQueryParam : BasePaginacaoQueryParam
    {
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
        public decimal PercentJurosMulta { get; set; }
        public decimal PercentValor { get; set; }
        public bool? Status { get; set; }
        public DateTime? MesAnoInicial { get; set; }
        public DateTime? MesAnoFinal { get; set; }
        public DateTime? ValidadeInicial { get; set; }
        public DateTime? ValidadeFinal { get; set; }
        public int[] Cursos { get; set; }
        public int[] Semestres { get; set; }
        public int[] TiposPagamentos { get; set; }
        public int[] SituacoesAlunos { get; set; }
        public int[] TiposTitulos { get; set; }
    }
}
