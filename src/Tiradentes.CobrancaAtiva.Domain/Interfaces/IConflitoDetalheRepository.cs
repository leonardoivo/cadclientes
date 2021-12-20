using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IConflitoDetalheRepository : IBaseRepository<ConflitoDetalheModel>
    {

        Task<IList<ConflitoDetalheModel>> BuscarPorIdConflitoComRelacionamentos(int idConflito);

    }
}
