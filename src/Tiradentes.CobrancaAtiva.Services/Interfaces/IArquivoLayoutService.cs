using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IArquivoLayoutService
    {
        Task<DateTime> SalvarLayoutArquivo(string status, RespostaViewModel arquivoResposta);
        Task AtualizarStatusLayoutArquivo(DateTime dataHora, string status);
        ArquivoLayoutViewModel BuscarPorDataHora(DateTime dataHora);


    }
}
