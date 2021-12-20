using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IConflitoService : IDisposable
    {
        Task<IList<ConflitoViewModel>> Buscar(Domain.QueryParams.ConflitoQueryParam queryParam);
    }
}
