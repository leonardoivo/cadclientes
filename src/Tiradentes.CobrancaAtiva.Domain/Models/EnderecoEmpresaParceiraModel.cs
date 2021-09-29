namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class EnderecoEmpresaParceiraModel : BaseModel
    {
        public EnderecoEmpresaParceiraModel()
        { }

        public EnderecoEmpresaParceiraModel(int id,
                                            string cep,
                                            string estado, 
                                            string cidade, 
                                            string logradouro,
                                            string numero,
                                            string complemento)
        {
            Id = id;
            CEP = cep;
            Estado = estado;
            Cidade = cidade;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
        }

        public string CEP { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public int EmpresaId { get; private set; }

        public EmpresaParceiraModel Empresa { get; private set; }
    }
}
