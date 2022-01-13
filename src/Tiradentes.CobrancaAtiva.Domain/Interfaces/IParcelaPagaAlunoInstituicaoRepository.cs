
namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IParcelaPagaAlunoInstituicaoRepository
    {
        bool ParcelaPagaInstituicao(string tipoInadimplencia, string sistema, decimal matricula, decimal periodo, int? parcela, decimal? idTitulo, int? codigoAtividade, decimal? numeroEvt, decimal? idPessoa, int? codigoBanco, int? codigoAgencia, decimal? numeroConta, decimal? numeroCheque);
    }
}
