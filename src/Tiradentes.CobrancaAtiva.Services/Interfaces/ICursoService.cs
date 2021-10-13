using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ICursoService : IDisposable
    {
        Task<IList<CursoViewModel>> Buscar();
        Task<IList<CursoViewModel>> BuscarPorInstituicaoModalidade(int instituicaoId, int modalidadeId);
    }
}
