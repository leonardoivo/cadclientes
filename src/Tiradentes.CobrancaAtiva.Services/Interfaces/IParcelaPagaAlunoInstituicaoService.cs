
using System;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IParcelaPagaAlunoInstituicaoService : IDisposable
    {
        bool ParcelaPagaInstituicao(string tipoInadimplencia,
                            string sistema,   
                            decimal matricula,
                            decimal periodo,
                            int? parcela,
                            decimal? idTitulo,
                            int? codigoAtividade,
                            decimal? numeroEvt,
                            decimal? idPessoa,                            
                            int? codigoBanco,
                            int? codigoAgencia,
                            decimal? numeroConta,
                            decimal? numeroCheque);
    }
}
