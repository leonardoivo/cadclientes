using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.SituacaoAluno;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ISituacaoAlunoService : IDisposable
    {
        Task<IList<SituacaoAlunoViewModel>> Buscar();
    }
}
