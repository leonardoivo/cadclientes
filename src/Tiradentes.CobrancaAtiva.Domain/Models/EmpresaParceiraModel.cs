using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class EmpresaParceiraModel : BaseModel
    {
        public EmpresaParceiraModel() { }

        public EmpresaParceiraModel(
            string NomeFantasia, string RazaoSocial, 
            string Sigla, string CNPJ, string NumeroContrato,
            string URL, bool Status)
        {
            this.NomeFantasia = NomeFantasia;
            this.RazaoSocial = RazaoSocial;
            this.Sigla = Sigla;
            this.CNPJ = CNPJ;
            this.NumeroContrato = NumeroContrato;
            this.URL = URL;
            this.Status= Status;
        }

        public string NomeFantasia { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Sigla { get; private set; }
        public string CNPJ { get; private set; }
        public string NumeroContrato { get; private set; }
        public string AditivoContrato { get; private set; }
        public string URL { get; private set; }
        public bool Status { get; private set; }

        public string TipoEnvioArquivo { get; private set; }
        public string IpEnvioArquivo { get; private set; }
        public int? PortaEnvioArquivo { get; private set; }
        public string UsuarioEnvioArquivo { get; private set; }
        public string SenhaEnvioArquivo { get; private set; }

        public ICollection<ContatoEmpresaParceiraModel> Contatos { get; private set; }
        public ICollection<HonorarioEmpresaParceiraModel> Honorarios { get; private set; }
        public ICollection<ParametroEnvioModel> ParametroEnvios { get; private set; }
        public EnderecoEmpresaParceiraModel Endereco { get; private set; }
        public ContaBancariaEmpresaParceiraModel ContaBancaria { get; private set; }

        public void SetarEndereco(int id,
                                    string cep,
                                    string estado,
                                    string cidade,
                                    string logradouro,
                                    string numero,
                                    string complemento)
        {
            this.Endereco = new EnderecoEmpresaParceiraModel(id, cep, estado, cidade, logradouro,
                                                            numero, complemento);
        }

        public void SetarContaBancaria(int Id,
            string ContaCorrente,
            string CodigoAgencia,
            string Convenio,
            string Pix,
            int BancoId)
        { 
            this.ContaBancaria = new ContaBancariaEmpresaParceiraModel(Id, ContaCorrente, CodigoAgencia, Convenio,
                                                                        Pix, BancoId);
        }
    }
}
