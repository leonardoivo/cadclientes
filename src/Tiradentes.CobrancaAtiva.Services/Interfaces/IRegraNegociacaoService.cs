using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Domain.DTO;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IRegraNegociacaoService
    {
        Task InativarRegrasNegociacao();
        Task AtivarRegrasNegociacao();
        Task<ViewModelPaginada<BuscaRegraNegociacaoViewModel>> Buscar(ConsultaRegraNegociacaoQueryParam queryParam);
        Task<BuscaRegraNegociacaoViewModel> BuscarPorId(int id);
        Task<RegraNegociacaoViewModel> Criar(CriarRegraNegociacaoViewModel viewModel);
        Task<RegraNegociacaoViewModel> Alterar(AlterarRegraNegociacaoViewModel viewModel);
        Task<RegraNegociacaoViewModel> VerificarRegraConflitante(CriarRegraNegociacaoViewModel viewModel);
    }
}
