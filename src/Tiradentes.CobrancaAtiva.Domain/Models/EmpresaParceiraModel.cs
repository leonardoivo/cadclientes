using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class EmpresaParceiraModel : BaseModel
    {
        public EmpresaParceiraModel() { }

        public string NomeFantasia { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Sigla { get; private set; }
        public string CNPJ { get; private set; }
        public string NumeroContrato { get; private set; }
        public string URL { get; private set; }
        public bool Status { get; private set; }

        public ICollection<ContatoEmpresaParceiraModel> Contatos { get; private set; }
        public EnderecoEmpresaParceiraModel Endereco { get; private set; }

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
    }
}
