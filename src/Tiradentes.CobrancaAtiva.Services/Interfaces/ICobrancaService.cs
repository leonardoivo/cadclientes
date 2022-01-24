using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ICobrancaService : IDisposable
    {
        Task<RespostaViewModel> Criar(RespostaViewModel model);

        Task<IEnumerable<RespostaViewModel>> BuscarRepostaNaoIntegrada();

        RespostaViewModel AlterarStatus(RespostaViewModel viewModel);
    }
}
