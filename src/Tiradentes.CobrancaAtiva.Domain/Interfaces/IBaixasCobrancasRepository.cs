using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IBaixasCobrancasRepository : IBaseRepository<BaixasCobrancasModel>
    {
        Task<BaixasCobrancasModel> BuscarPorDataBaixa(DateTime dataBaixa);
        void HabilitarAlteracaoBaixaCobranca(bool status);
        Task<ModelPaginada<BuscaBaixaPagamentoDto>> Buscar(BaixaCobrancaQueryParam queryParam);
    }
}
