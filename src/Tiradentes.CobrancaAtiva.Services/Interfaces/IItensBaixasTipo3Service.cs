using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IItensBaixasTipo3Service : IDisposable
    {
        Task InserirBaixa(DateTime dataBaixa,
                          decimal matricula,
                          decimal numeroAcordo,
                          int parcela,
                          DateTime dataPagamento,
                          decimal valorPago,
                          decimal? codErro);
    }
}
