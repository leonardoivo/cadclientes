using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IEmpresaParceiraRepository : IBaseRepository<EmpresaParceiraModel>
    {
        Task<bool> VerificaCnpjJaCadastrado(string Cnpj);
        Task<IList<EmpresaParceiraModel>> Buscar(EmpresaParceiraQueryParam queryParams);
    }
}
