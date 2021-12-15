using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IRegraNegociacaoRepository : IBaseRepository<RegraNegociacaoModel>
    {
        Task<List<RegraNegociacaoModel>> ListarRegrasParaInativar();
        Task<List<RegraNegociacaoModel>> ListarRegrasParaAtivar();
        Task<ModelPaginada<BuscaRegraNegociacao>> Buscar(RegraNegociacaoQueryParam queryParam);
        Task<BuscaRegraNegociacao> BuscarPorIdComRelacionamentos(int id);
        Task<RegraNegociacaoModel> VerificarRegraConflitante(RegraNegociacaoModel model);
    }
}
