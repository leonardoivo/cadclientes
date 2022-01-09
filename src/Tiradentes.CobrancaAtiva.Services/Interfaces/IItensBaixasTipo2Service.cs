﻿using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IItensBaixasTipo2Service : IDisposable
    {
        Task InserirBaixa(DateTime dataBaixa, decimal matricula, int numeroAcordo, int parcela, int periodo, DateTime dataVencimento, decimal valor, decimal? codErro);
    }
}
