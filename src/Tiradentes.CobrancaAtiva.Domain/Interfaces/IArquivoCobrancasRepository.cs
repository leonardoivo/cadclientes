using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IArquivoCobrancasRepository
    {
        Task Criar(ArquivoCobrancasModel model);
    }
}