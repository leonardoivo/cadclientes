using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IAcordoCobrancasRepository : IBaseRepository<AcordosCobrancasModel>
    {
        bool ExisteAcordo(decimal numeroAcordo);
        Task AtualizarMatriculaAcordo(decimal matricula, decimal numeroAcordo);
        Task AtualizarSaldoDevedor(decimal numeroAcordo, decimal valor);

    }
}
