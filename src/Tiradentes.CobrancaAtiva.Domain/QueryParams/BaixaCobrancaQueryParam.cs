namespace Tiradentes.CobrancaAtiva.Domain.QueryParams
{
    public class BaixaCobrancaQueryParam : BasePaginacaoQueryParam
    {
        public string Acordo { get; set; }

        public string Cpf { get; set; }

        public string Matricula { get; set; }

        public string NomeAluno { get; set; }
    }
}
