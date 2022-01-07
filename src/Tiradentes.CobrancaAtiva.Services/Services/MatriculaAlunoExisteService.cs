using System;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class MatriculaAlunoExisteService : IMatriculaAlunoExisteService
    {
        IMatriculaAlunoExisteRepository _repository;
        public MatriculaAlunoExisteService(IMatriculaAlunoExisteRepository repository)
        {
            _repository = repository;
        }
        public bool MatriculaAlunoExiste(string tipoInadimplencia, string sistema, decimal matricula)
        {
            return _repository.MatriculaAlunoExiste(tipoInadimplencia,
                                                    sistema,
                                                    matricula);
        }
    }
}
