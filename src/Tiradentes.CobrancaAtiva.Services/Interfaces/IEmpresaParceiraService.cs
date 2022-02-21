using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IEmpresaParceiraService : IDisposable
    {
        Task VerificarCnpjJaCadastrado(string cnpj, int? id);
        Task<ViewModelPaginada<BuscaEmpresaParceiraViewModel>> Buscar(ConsultaEmpresaParceiraQueryParam queryParams);
        Task<EmpresaParceiraViewModel> Criar(EmpresaParceiraViewModel viewModel);
        Task<EmpresaParceiraViewModel> Atualizar(EmpresaParceiraViewModel viewModel);
        Task Deletar(int id);
        Task<EmpresaParceiraViewModel> BuscarPorId(int Id);
        Task<EmpresaParceiraViewModel> BuscarPorCnpj(string cnpj);
    }
}
