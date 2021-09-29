using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task BuscarPorCep();
    }
}
