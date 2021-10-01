using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IRegraNegociacaoRepository : IBaseRepository<RegraNegociacaoModel>
    {
        Task<IList<BuscaRegraNegociacao>> BuscarT();
    }
}
