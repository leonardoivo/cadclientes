using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IArquivoLayoutService
    {
        Task<DateTime> SalvarLayoutArquivo(string status, RespostaViewModel arquivoResposta);
        Task AtualizarStatusLayoutArquivo(DateTime dataHora, string status);


    }
}
