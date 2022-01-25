using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Collections;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IApplicationErrorRepository
    {
        Task Insert(ApplicationErrorCollection model);
    }
}
