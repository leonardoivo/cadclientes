﻿using System;
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
        public bool ParcelaPagaInstituicao(string tipoInadimplencia, string sistema, decimal matricula, decimal periodo, int? parcela, decimal? idTitulo, int? codigoAtividade, decimal? numeroEvento, decimal? idPessoa, int? codigoBanco, int? codigoAgencia, decimal? numeroConta, decimal? numeroCheque)
        {
            return _repository.ParcelaPagaInstituicao(tipoInadimplencia,
                                                      sistema,
                                                      matricula,
                                                      periodo,
                                                      parcela,
                                                      idTitulo,
                                                      codigoAtividade,
                                                      numeroEvento,
                                                      idPessoa,
                                                      codigoBanco,
                                                      codigoAgencia,
                                                      numeroConta,
                                                      numeroCheque);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository?.Dispose();
            }
        }

    }
}
