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

        public string CEP { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int EmpresaId { get; set; }

        public EmpresaParceiraModel Empresa { get; set; }
    }
}
