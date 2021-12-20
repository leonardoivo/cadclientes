using System;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Domain.Enums;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito
{
    public class ConflitoDetalheViewModel
    {
        public int Id { get; set; }
        public int ConflitoId { get; set; }
        public int ModalidadeId { get; set; }
        public string Parcela { get; set; }
        public decimal Valor { get; set; }
        public TipoConflito TipoConflito { get; set; }
        public DateTime DataEnvio { get; set; }

        public ConflitoViewModel Conflito { get; set; }
    }
}