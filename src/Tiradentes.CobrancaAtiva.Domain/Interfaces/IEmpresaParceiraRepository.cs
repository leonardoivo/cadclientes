using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.DTO;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IEmpresaParceiraRepository : IBaseRepository<EmpresaParceiraModel>
    {
        Task<bool> VerificaCnpjJaCadastrado(string Cnpj);
        Task<ModelPaginada<EmpresaParceiraModel>> Buscar(EmpresaParceiraQueryParam queryParams);
    }
}
