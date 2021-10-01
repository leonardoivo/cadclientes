using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IRegraNegociacaoService
    {
        Task<IList<BuscaRegraNegociacaoViewModel>> Buscar();
        Task<RegraNegociacaoViewModel> Criar(CriarRegraNegociacaoViewModel viewModel);
    }
}
