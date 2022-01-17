using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Enum;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IErroLayoutService : IDisposable
    {
        Task<decimal?> RegistrarErro(ErrosBaixaPagamento erro, RespostaViewModel conteudo);
        List<ErroLayoutViewModel> BuscarPorDataHora(DateTime dataHora);
    }
}
