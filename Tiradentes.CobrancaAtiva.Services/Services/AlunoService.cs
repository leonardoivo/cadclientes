using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiradentes.CobrancaAtiva.Entities.Dto.Aluno;
using Tiradentes.CobrancaAtiva.Repositories;
using Tiradentes.CobrancaAtiva.Repositories.Interface.Aluno;
using Tiradentes.CobrancaAtiva.Repositories.Models.Aluno;
using Tiradentes.CobrancaAtiva.Services.Interface.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _repository;

        public AlunoService(IAlunoRepository repository) => _repository = repository;

        public void Save(AlunoDto aluno)
        {
            _repository.Save(aluno);
        }

        public IEnumerable<AlunoDto> List()
        {
            return _repository.List();
        }
    }
}
