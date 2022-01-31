﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca
{
    public class RegularizarParcelasAcordoViewModel
    {
        public Int64 CnpjEmpresaCobranca { get; set; }
        public Int64 NumeroAcordo { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPago { get; set; }
        public decimal Parcela { get; set; }
        public string Texto { get; set; }
    }
}
