using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IItensBaixasTipo3Repository : IBaseRepository<ItensBaixaTipo3Model>
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
