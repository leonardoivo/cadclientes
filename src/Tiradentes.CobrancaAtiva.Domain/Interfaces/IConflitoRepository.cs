using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;
using System;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IConflitoRepository : IBaseRepository<ConflitoModel>
    {

        Task<ModelPaginada<BuscaConflito>> Buscar(ConflitoQueryParam queryParam);
        Task<IEnumerable<ConflitoModel>> BuscarPorLote(Guid lote);

    }
}
