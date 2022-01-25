using System;
using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca
{
    public class ArquivoLayoutViewModel
    {
        public DateTime DataHora { get; set; }
        public string NomeUsuario { get; set; }
        public string Status { get; set; }
        public string Conteudo { get; set; }

        public List<ErroLayoutViewModel> ErrosLayout { get; set; }
    }
}
