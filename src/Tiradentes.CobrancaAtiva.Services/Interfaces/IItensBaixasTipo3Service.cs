using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IItensBaixasTipo3Service : IDisposable
    {
        Task InserirBaixa(DateTime dataBaixa,
                          decimal matricula,
                          int numeroAcordo,
                          int parcela,
                          DateTime dataPagamento,
                          decimal valorPago,
                          int codErro);
    }
}
