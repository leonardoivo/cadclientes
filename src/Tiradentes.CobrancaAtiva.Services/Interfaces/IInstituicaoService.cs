using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IInstituicaoService : IDisposable
    {
        Task<IList<InstituicaoViewModel>> Buscar();
    }
}
