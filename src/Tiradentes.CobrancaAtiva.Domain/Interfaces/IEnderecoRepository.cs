using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<EnderecoModel> BuscarPorCep(string CEP);
    }
}
