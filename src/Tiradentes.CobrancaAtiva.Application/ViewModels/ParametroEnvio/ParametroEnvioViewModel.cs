using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio
{
    public class ParametroEnvioViewModel
    {
        public int InstituicaoId { get; set; }
        public int ModedalidadeId { get; set; }
        public int EmpresaId { get; set; }
        public int DiaEnvio { get; set; }
        public bool Status { get; set; }
        public DateTime Validade { get; set; }
    }
}
