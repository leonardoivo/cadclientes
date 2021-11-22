using System;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio
{
    public class ParametroEnvioViewModel
    {
        public int EmpresaId { get; set; }
        public int InstituicaoId { get; set; }
        public int ModalidadeId { get; set; }
        public int DiaEnvio { get; set; }
        public bool Status { get; set; }
        public DateTime Validade { get; set; }
    }
}
