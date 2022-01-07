using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IParcelasAcordoRepository : IBaseRepository<ParcelasAcordoModel>
    {
        bool ExisteParcelaAcordo(decimal parcela, decimal numeroAcordo);
        bool ExisteParcelaPaga(decimal numeroAcordo);
        Task QuitarParcelasAcordo(decimal numeroAcordo);
        decimal ObterValorParcelaAcordo(decimal parcela, decimal numeroAcordo);
        Task AtualizaPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataPagamento, DateTime dataBaixa, decimal valorPago);
        Task EstornarParcelaAcordo(decimal parcela, decimal numeroAcordo);
    }
}
