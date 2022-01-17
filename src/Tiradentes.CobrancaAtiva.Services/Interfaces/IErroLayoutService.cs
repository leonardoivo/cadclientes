using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Enum;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IErroLayoutService : IDisposable
    {
        List<ErroLayoutViewModel> BuscarPorDataHora(DateTime dataHora);

        Task<decimal?> CriarErroLayoutService(DateTime dataHora, ErrosBaixaPagamento erro, string descricao);
    }
}
