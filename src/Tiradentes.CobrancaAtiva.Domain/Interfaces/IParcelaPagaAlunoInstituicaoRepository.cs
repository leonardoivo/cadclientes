﻿
namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IParcelaPagaAlunoInstituicaoRepository
    {
        bool ParcelaPagaInstituicao(string tipoInadimplencia,
                                    string sistema,
                                    decimal? idAluno,
                                    int? ano,
                                    int? semestre,
                                    int? parcela,
                                    decimal? idTitulo,
                                    int? codigoAtividade,
                                    decimal? numeroEvt,
                                    decimal? idDDP,
                                    decimal? idTituloAvulso,
                                    decimal? codigoBanco,
                                    decimal? codigoAgencia,
                                    decimal? numeroConta,
                                    decimal? numeroCheque);
    }
}
