using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IEmpresaParceiraService
    {
        Task VerificarCnpjJaCadastrado(string Cnpj);
        Task<ViewModelPaginada<BuscaEmpresaParceiraViewModel>> Buscar(ConsultaEmpresaParceiraQueryParam queryParams);
        Task<EmpresaParceiraViewModel> Criar(EmpresaParceiraViewModel viewModel);
        Task<EmpresaParceiraViewModel> Atualizar(EmpresaParceiraViewModel viewModel);
        Task Deletar(EmpresaParceiraViewModel viewModel);
        Task<EmpresaParceiraViewModel> BuscarPorId(int Id);
    }
}
