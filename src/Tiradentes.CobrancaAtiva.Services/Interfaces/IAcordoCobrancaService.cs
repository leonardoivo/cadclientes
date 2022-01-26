﻿using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IAcordoCobrancaService : IDisposable
    {
        bool ExisteAcordo(decimal numeroAcordo);
        Task AtualizarSaldoDevedor(decimal numeroAcordo, decimal valor);
        Task AtualizarMatriculaAcordo(decimal matricula, decimal numeroAcordo);
        Task InserirAcordoCobranca(decimal numeroAcordo, DateTime dataBaixa, DateTime dataAcordo, int totalParcelas, decimal valorTotal, decimal multa, decimal juros, decimal matricula, decimal saldoDevedor, string cpf, string cnpjEmpresaCobranca, string sistema, string tipoInadimplencia);

        decimal ObterMatricula(decimal numeroAcordo);


    }
}