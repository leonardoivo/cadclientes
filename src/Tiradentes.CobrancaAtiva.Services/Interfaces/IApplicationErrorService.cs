using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Domain.Collections;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IApplicationErrorService : IDisposable
    {
        Task LogError(ApplicationErrorCollection model);
    }
}
