
using System;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IMatriculaAlunoExisteService : IDisposable
    {
        bool MatriculaAlunoExiste(string tipoInadimplencia, string sistema, decimal matricula);
    }
}
