using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.DTO;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ICobrancaService : IDisposable
    {
        Task<RespostaViewModel> Criar(RespostaViewModel model);
        Task<ICollection<BaixaPagamento>> Listar(RespostaViewModel viewModel);
        Task<IEnumerable<string>> ListarFiltrosMatricula();
        Task<IEnumerable<string>> ListarFiltrosAcordo();
        Task<IEnumerable<string>> ListarFiltroCpf();
        Task<IEnumerable<string>> ListarFiltroNomeAluno();
    }
}
