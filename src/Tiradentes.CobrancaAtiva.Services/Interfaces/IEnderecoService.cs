using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Endereco;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<EnderecoViewModel> BuscarPorCep(string CEP);
    }
}
