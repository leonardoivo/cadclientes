using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Banco;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IBancoService : IDisposable
    {
        Task<IList<BancoViewModel>> Buscar();
    }
}
