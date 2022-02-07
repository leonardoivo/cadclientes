using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IBaixasCobrancasService
    {
        Task AtualizarBaixasCobrancas(BaixasCobrancasViewModel baixasCobrancas);
        Task CriarBaixasCobrancas(DateTime dataBaixa);
        Task<BaixasCobrancasViewModel> Buscar(DateTime dataBaixa);
    }
}