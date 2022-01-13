using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ParcelaPagaAlunoInstituicaoRepository : IParcelaPagaAlunoInstituicaoRepository
    {
        readonly IIdAlunoRepository _idAlunoRepository;
        readonly CobrancaAtivaDbContext _context;
        public ParcelaPagaAlunoInstituicaoRepository(IIdAlunoRepository idAlunoRepository,
                                                     CobrancaAtivaDbContext context)
        {
            _idAlunoRepository = idAlunoRepository;
            _context = context;
        }
        public bool ParcelaPagaInstituicao(string tipoInadimplencia, string sistema, decimal matricula, decimal periodo, int? parcela, decimal? idTitulo, int? codigoAtividade, decimal? numeroEvt, decimal? idPessoa, int? codigoBanco, int? codigoAgencia, decimal? numeroConta, decimal? numeroCheque)
        {
            ParcelaPagaAlunoInstituicaoModel parcelaAluno = new ParcelaPagaAlunoInstituicaoModel();

            var ano = periodo.ToString().Substring(1, 4);
            var semestre = periodo.ToString().Substring(4, 1);

            var idAluno = _idAlunoRepository.ObterIdAluno(matricula);

            if (tipoInadimplencia == "P")
            {
                if(sistema == "S")
                {
                    parcelaAluno = _context.ParcelaPagaAlunoInstituicaoModel
                                            .FromSqlRaw($@"select count(idt_alu) as Count
                                                             from sca.pgto_alunos
                                                            where idt_alu = {idAluno}
                                                              and ano = {ano}
                                                              and semestre = {semestre}
                                                              and parcela = {parcela}
                                                              and sta_pgto <> 'N'")
                                            .FirstOrDefault();
                }
                else if (sistema == "E")
                {
                    parcelaAluno = _context.ParcelaPagaAlunoInstituicaoModel
                                            .FromSqlRaw($@"select count(idt_alu) 
                                                             from profope.pgto_alunos 
                                                            where idt_alu = {idAluno}
                                                              and parcela = {parcela}
                                                              and sta_pgto <> 'N'")
                                            .FirstOrDefault();
                }
                else if (sistema == "P")
                {
                    parcelaAluno = _context.ParcelaPagaAlunoInstituicaoModel
                                            .FromSqlRaw($@"select count(idt_alu) 
                                                             from spgl.pgto_alunos 
                                                            where idt_alu = {idAluno}
                                                              and parcela = {parcela}
                                                              and sta_pgto <> 'N'")
                                            .FirstOrDefault();
                }
                else if (sistema == "I")
                {
                    parcelaAluno = _context.ParcelaPagaAlunoInstituicaoModel
                                            .FromSqlRaw($@"select count(id_alu)
                                                             from SCF.v_pgt_titulos
                                                            where idt_titulo = {idTitulo}
                                                              and dat_pgto is not null")
                                            .FirstOrDefault();
                }
            }
            else if (tipoInadimplencia == "T")
            {
                parcelaAluno = _context.ParcelaPagaAlunoInstituicaoModel
                                       .FromSqlRaw($@"select count(idt_titulo_avu) 
                                                        from scf.sap_titulos_avulsos 
                                                       where idt_titulo_avu = {idTitulo}
                                                         and dat_pgto is not null")
                                       .FirstOrDefault();
            }
            else if (tipoInadimplencia == "C")
            {
                parcelaAluno = _context.ParcelaPagaAlunoInstituicaoModel
                                       .FromSqlRaw($@"select count(num_cheque) 
                                                      from SCF.cheques 
                                                      where cod_banco   = {codigoBanco}
                                                        and cod_agencia = {codigoAgencia}
                                                        and num_conta   = {numeroConta}
                                                        and num_cheque  = {numeroCheque}
                                                        and dat_reg is not null")
                                       .FirstOrDefault();
            }

            return parcelaAluno.Count > 0;
        }

    }
}
