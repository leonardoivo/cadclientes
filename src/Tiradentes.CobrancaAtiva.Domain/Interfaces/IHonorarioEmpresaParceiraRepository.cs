using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IHonorarioEmpresaParceiraRepository : IBaseRepository<HonorarioEmpresaParceiraModel>
    {
        Task<ModelPaginada<HonorarioEmpresaParceiraModel>> Buscar(HonorarioEmpresaParceiraQueryParam queryParams);
    }
}
