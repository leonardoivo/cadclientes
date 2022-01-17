
using System;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IMatriculaAlunoExisteRepository : IDisposable
    {
        bool MatriculaAlunoExiste(string tipoInadimplencia, string sistema, decimal matricula);
    }
}
