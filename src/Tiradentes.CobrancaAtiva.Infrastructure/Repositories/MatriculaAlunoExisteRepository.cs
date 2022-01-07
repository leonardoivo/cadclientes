using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class MatriculaAlunoExisteRepository : IMatriculaAlunoExisteRepository
    {
        readonly CobrancaAtivaDbContext _context;
        public MatriculaAlunoExisteRepository(CobrancaAtivaDbContext context)
        {
            _context = context;
        }
        
        public bool MatriculaAlunoExiste(string tipoInadimplencia, string sistema, decimal matricula)
        {
            MatriculaAlunoExisteModel matriculaAluno = new MatriculaAlunoExisteModel();

            if (tipoInadimplencia == "P")
            {
                if(sistema == "S")
                {
                    matriculaAluno = _context.MatriculaAlunoExisteModel
                        .FromSqlRaw($@"select count(idt_alu) as Count
                                         from sca.v_alunos where mat_alu = {matricula}").FirstOrDefault();
                }
                else if(sistema == "E")
                {
                    matriculaAluno = _context.MatriculaAlunoExisteModel
                        .FromSqlRaw($@"select count(idt_alu) as Count
                                         from profope.v_alunos where mat_alu = {matricula}").FirstOrDefault();
                }
                else if (sistema == "P")
                {
                    matriculaAluno = _context.MatriculaAlunoExisteModel
                        .FromSqlRaw($@"select count(idt_alu) as Count
                                         from spgl.v_alunos where mat_alu = {matricula}").FirstOrDefault();
                }
                else if (sistema == "I")
                {
                    matriculaAluno = _context.MatriculaAlunoExisteModel
                        .FromSqlRaw($@"select count(idt_alu) as Count
                                        from sip.v_alunos where mat_alu = {matricula}").FirstOrDefault();
                }
                else if (sistema == "X")
                {
                    matriculaAluno = _context.MatriculaAlunoExisteModel
                        .FromSqlRaw($@"select count(idt_ddp) as Count
                                        from extensao.v_pre_inscricoes where cpf = {matricula}").FirstOrDefault();
                }
            }
            else if(tipoInadimplencia == "T")
            {
                matriculaAluno = _context.MatriculaAlunoExisteModel
                        .FromSqlRaw($@"select count(idt_titulo_avu) as Count
                                        from scf.sap_titulos_avulsos where cpf_cnpj = {matricula}").FirstOrDefault();
            }
            else if(tipoInadimplencia == "C")
            {
                matriculaAluno = _context.MatriculaAlunoExisteModel
                        .FromSqlRaw($@"select count(num_cheque) as Count
                                        from SCF.cheques where cpf_cgc = {matricula}").FirstOrDefault();
            }
            else if(tipoInadimplencia == "R")
            {
                matriculaAluno = _context.MatriculaAlunoExisteModel
                        .FromSqlRaw($@"select count(*) as Count
                                        from scf.itens_geracao where matricula = {matricula}").FirstOrDefault();
            }

            return matriculaAluno.Count > 0;
        }
    }
}
