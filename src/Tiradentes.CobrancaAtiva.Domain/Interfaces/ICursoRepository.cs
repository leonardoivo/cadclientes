using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface ICursoRepository : IBaseRepository<CursoModel>
    {
        Task<IList<CursoModel>> BuscarPorInstituicaoModalidade(int instituicaoId, int modalidadeId);
    }
}
