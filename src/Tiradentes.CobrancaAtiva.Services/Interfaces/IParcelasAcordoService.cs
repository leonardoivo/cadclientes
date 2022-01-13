﻿using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IParcelasAcordoService : IDisposable
    {
        bool ExisteParcelaAcordo(decimal parcela, decimal numeroAcordo);
        bool ExisteParcelaPaga(decimal numeroAcordo);
        bool ParcelaPaga(decimal parcela, decimal numeroAcordo);
        Task QuitarParcelasAcordo(decimal numeroAcordo, decimal matricula, string sistema, DateTime dataPagamento, decimal periodo, decimal? idTitulo, int? codigoAtividade, int? numeroEvt, decimal? idPessoa, int codigobanco, int codigoAgencia, int numeroConta, decimal numeroCheque, string CpfCnpj);
        decimal? ObterValorParcelaAcordo(decimal parcela, decimal numeroAcordo);
        Task AtualizaPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataPagamento, DateTime dataBaixa, decimal valorPago);
        Task InserirPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataVencimento, DateTime dataBaixa, decimal valorPago);
        Task EstornarParcelaAcordo(decimal parcela, decimal numeroAcordo);
    }
}
