using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ISituacaoService : IDisposable
    {
        Task<IList<SituacaoViewModel>> Buscar();
    }
}
