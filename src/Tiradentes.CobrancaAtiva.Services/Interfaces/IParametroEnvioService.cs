using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Domain.DTO;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IParametroEnvioService
    {
        Task<ViewModelPaginada<BuscaParametroEnvioViewModel>> Buscar(ConsultaParametroEnvioQueryParam queryParam);
        Task<BuscaParametroEnvioViewModel> BuscarPorId(int id);
        Task<ParametroEnvioViewModel> Criar(CriarParametroEnvioViewModel viewModel);
        Task<ParametroEnvioViewModel> Alterar(AlterarParametroEnvioViewModel viewModel);
        Task Deletar(int id);
    }
}
