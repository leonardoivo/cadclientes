using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IitensBaixasTipo3Repository : IBaseRepository<ItensBaixaTipo3Model>
    {
        Task InserirBaixa(DateTime dataBaixa,
                          decimal matricula,
                          int numeroAcordo,
                          int parcela,
                          DateTime dataPagamento,
                          decimal valorPago,
                          decimal? codErro);
    }
}
