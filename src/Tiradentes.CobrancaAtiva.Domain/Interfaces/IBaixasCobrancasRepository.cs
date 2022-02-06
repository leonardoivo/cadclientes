using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IBaixasCobrancasRepository : IBaseRepository<BaixasCobrancasModel>
    {
        Task<BaixasCobrancasModel> BuscarPorDataBaixa(DateTime dataBaixa);
        void HabilitarAlteracaoBaixaCobranca(bool status);
        Task<object> Buscar();
    }
}
