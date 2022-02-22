using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IParametroEnvioRepository : IBaseRepository<ParametroEnvioModel>
    {
        Task<ModelPaginada<BuscaParametroEnvio>> Buscar(ParametroEnvioQueryParam queryParam);
        Task<BuscaParametroEnvio> BuscarPorIdComRelacionamentos(int id);
        Task<List<ParametroEnvioModel>> BuscarApenasParametroEnvio();
    }
}
