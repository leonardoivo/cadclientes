using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira
{
    public class EmpresaParceiraViewModel
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Sigla { get; set; }
        public string CNPJ { get; set; }
        public string NumeroContrato { get; set; }
        public string AditivoContrato { get; set; }
        public string URL { get; set; }
        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int BancoId { get; set; }
        public string ContaCorrente { get; set; }
        public string CodigoAgencia { get; set; }
        public string Convenio { get; set; }
        public string Pix { get; set; }
        public bool Status { get; set; }
        public string TipoEnvioArquivo { get; set; }
        public string IpEnvioArquivo { get; set; }
        public int? PortaEnvioArquivo { get; set; }
        public string UsuarioEnvioArquivo { get; set; }
        public string SenhaEnvioArquivo { get; set; }

        public List<ContatoEmpresaParceiraViewModel> Contatos { get; set; }
    }
}
