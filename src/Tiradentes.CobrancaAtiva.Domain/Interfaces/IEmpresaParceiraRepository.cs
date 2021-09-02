using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IEmpresaParceiraRepository : IBaseRepository<EmpresaParceiraModel>
    {
        Task<bool> VerificaCnpjJaCadastrado(string Cnpj) ;
    }
}
