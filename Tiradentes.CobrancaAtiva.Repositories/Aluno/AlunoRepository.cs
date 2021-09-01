using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tiradentes.CobrancaAtiva.Entities.Dto.Aluno;
using Tiradentes.CobrancaAtiva.Repositories.Interface.Aluno;
using Tiradentes.CobrancaAtiva.Repositories.Models.Aluno;

namespace Tiradentes.CobrancaAtiva.Repositories.Aluno
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IMapper _mapper;
        private readonly TesteContext _context;

        public AlunoRepository(
            IMapper mapper,
            TesteContext context
        )
        {
            this._mapper = mapper;
            this._context = context;
        }

        public void Save(AlunoDto aluno)
        {
            var model = _mapper.Map<AlunoModel>(aluno);

            _context.Alunos.Add(model);
            _context.SaveChanges();
        }

        public IEnumerable<AlunoDto> List()
        {
            return _mapper.Map<IEnumerable<AlunoModel>, List<AlunoDto>>(_context.Alunos);
        }
    }
}
