namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class EnderecoModel
    {
        public EnderecoModel()
        { }

        public EnderecoModel(string CEP,
                            string Estado,
                            string Cidade,
                            string Bairro,
                            string Logradouro)
        { 
            this.CEP = CEP;
            this.Estado = Estado;
            this.Cidade = Cidade;
            this.Bairro = Bairro;
            this.Logradouro = Logradouro;
        }

        public string CEP { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Bairro { get; private set; }
        public string Logradouro { get; private set; }
    }
}
