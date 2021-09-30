using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IInstituicaoModalidadeRegraRepository : IBaseRepository<InstituicaoModalidadeRegraModel>
    {
        Task<ModelPaginada<InstituicaoModalidadeRegraModel>> Buscar(InstituicaoModalidadeRegraQueryParam queryParams);
    }
}
