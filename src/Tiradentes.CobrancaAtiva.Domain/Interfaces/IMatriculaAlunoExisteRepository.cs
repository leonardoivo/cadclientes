
namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IMatriculaAlunoExisteRepository
    {
        bool MatriculaAlunoExiste(string tipoInadimplencia, string sistema, decimal matricula);
    }
}
