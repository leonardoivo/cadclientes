using System;
using System.Collections.Generic;
using System.Text;
using Tiradentes.CobrancaAtiva.Entities.Dto.Aluno;

namespace Tiradentes.CobrancaAtiva.Repositories.Interface.Aluno
{
    public interface IAlunoRepository
    {
        void Save(AlunoDto aluno);

        IEnumerable<AlunoDto> List();
    }
}
