
using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class IdAlunoRepository : IIdAlunoRepository
    {
        CobrancaAtivaDbContext _context;
        public IdAlunoRepository(CobrancaAtivaDbContext context)
        {
            _context = context;
        }
        public decimal ObterIdAluno(decimal matriculaAluno)
        {
            return _context.IdAlunoModel.FromSqlRaw($@" SELECT idt_alu 
                                                         FROM sca.alunos
                                                         WHERE mat_alu = {matriculaAluno}").FirstOrDefaultAsync().Id;
        }
    }
}
