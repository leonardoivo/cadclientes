using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.TituloAvulso;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ITituloAvulsoService : IDisposable
    {
        Task<IList<TituloAvulsoViewModel>> Buscar();
    }
}
