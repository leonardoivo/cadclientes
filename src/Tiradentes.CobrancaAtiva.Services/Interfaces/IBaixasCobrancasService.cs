using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IBaixasCobrancasService
    {
        Task AtualizarBaixasCobrancas(BaixasCobrancasViewModel baixasCobrancas);
    }
}
