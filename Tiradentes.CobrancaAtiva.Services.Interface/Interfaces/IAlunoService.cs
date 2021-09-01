using System;
using System.Collections.Generic;
using System.Text;
using Tiradentes.CobrancaAtiva.Entities.Dto.Aluno;

namespace Tiradentes.CobrancaAtiva.Services.Interface.Interfaces
{
    public interface IAlunoService
    {
        void Save(AlunoDto aluno);

        IEnumerable<AlunoDto> List();
    }
}
