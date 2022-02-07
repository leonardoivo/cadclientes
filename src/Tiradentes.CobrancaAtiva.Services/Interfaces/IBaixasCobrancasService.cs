using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IBaixasCobrancasService
    {
        Task AtualizarBaixasCobrancas(BaixasCobrancasViewModel baixasCobrancas);
        Task CriarBaixasCobrancas(DateTime dataBaixa);
        Task<BaixasCobrancasViewModel> Buscar(DateTime dataBaixa);
        Task<ViewModelPaginada<ConsultaBaixaPagamentoViewModel>> Buscar(ConsultaBaixaCobrancaQueryParam queryParams);
    }
}
