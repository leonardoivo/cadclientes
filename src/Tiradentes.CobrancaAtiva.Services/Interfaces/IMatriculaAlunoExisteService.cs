
namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IMatriculaAlunoExisteService
    {
        bool MatriculaAlunoExiste(string tipoInadimplencia, string sistema, decimal matricula);
    }
}
