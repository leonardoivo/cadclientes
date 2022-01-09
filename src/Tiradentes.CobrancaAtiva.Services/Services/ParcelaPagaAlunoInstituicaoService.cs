using System;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ParcelaPagaAlunoInstituicaoService : IParcelaPagaAlunoInstituicaoService
    {
        readonly IParcelaPagaAlunoInstituicaoRepository _repository;
        public ParcelaPagaAlunoInstituicaoService(IParcelaPagaAlunoInstituicaoRepository repository)
        {
            _repository = repository;
        }
        public bool ParcelaPagaInstituicao(string tipoInadimplencia, string sistema, decimal? idAluno, int? ano, int? semestre, int? parcela, decimal? idTitulo, int? codigoAtividade, decimal? numeroEvt, decimal? idDDP, decimal? idTituloAvulso, decimal? codigoBanco, decimal? codigoAgencia, decimal? numeroConta, decimal? numeroCheque)
        {
            return _repository.ParcelaPagaInstituicao(tipoInadimplencia,
                                                      sistema,
                                                      idAluno,
                                                      ano,
                                                      semestre,
                                                      parcela,
                                                      idTitulo,
                                                      codigoAtividade,
                                                      numeroEvt,
                                                      idDDP,
                                                      idTituloAvulso,
                                                      codigoBanco,
                                                      codigoAgencia,
                                                      numeroConta,
                                                      numeroCheque);
        }
    }
}
