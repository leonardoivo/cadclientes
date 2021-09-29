using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        Task BuscarPorCep();
    }
}
