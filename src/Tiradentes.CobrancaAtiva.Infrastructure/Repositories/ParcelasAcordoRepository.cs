using System;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Tiradentes.CobrancaAtiva.Domain.Collections;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ParcelasAcordoRepository : BaseRepository<ParcelasAcordoModel>, IParcelasAcordoRepository
    {
        readonly IIdAlunoRepository _idAlunoRepository;
        readonly IParcelaTituloRepository _parcelaTituloRepository;
        public ParcelasAcordoRepository(IIdAlunoRepository idAlunoRepository,
            IParcelaTituloRepository parcelaTituloRepository,
            CobrancaAtivaDbContext context) : base(context)
        {
            _idAlunoRepository = idAlunoRepository;
            _parcelaTituloRepository = parcelaTituloRepository;
        }


        private void HabilitarAlteracaoParcelasAcordo(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_parc_acord({status.ToString().ToLower()});
                                         end;
                                         ");
        }

        private void HabilitarAlteracaoAcordoCobrancas(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_acord_cob({status.ToString().ToLower()});
                                         end;
                                         ");
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_saldo({status.ToString().ToLower()});
                                         end;
                                         ");
        }

        public async Task EstornarParcelaAcordo(decimal parcela, decimal numeroAcordo)
        {
            var parcAcordo = DbSet.Where(P => P.Parcela == parcela
                                              && P.NumeroAcordo == numeroAcordo).FirstOrDefault();

            if (parcAcordo == null)
                return;

            parcAcordo.DataPagamento = null;
            parcAcordo.DataBaixaPagamento = null;
            parcAcordo.ValorPago = null;

            HabilitarAlteracaoParcelasAcordo(true);

            await Alterar(parcAcordo);

            HabilitarAlteracaoParcelasAcordo(false);
        }

        public bool ExisteParcelaAcordo(decimal parcela, decimal numeroAcordo)
        {
            return DbSet.Where(P => P.Parcela == parcela
                                    && P.NumeroAcordo == numeroAcordo).Count() > 0;
        }

        public bool ExisteParcelaPaga(decimal numeroAcordo)
        {
            return DbSet.Where(P => P.NumeroAcordo == numeroAcordo
                                    && P.DataPagamento != null
                                    && P.ValorPago != null).Count() > 0;
        }

        public async Task AtualizarPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataPagamento, DateTime dataBaixa, decimal valorPago, string situacaoPagamento)
        {
            var parcInserir = DbSet.Where(P => P.NumeroAcordo == numeroAcordo
                                               && P.Parcela == parcela).FirstOrDefault();

            parcInserir.DataPagamento = dataPagamento;
            parcInserir.ValorPago = valorPago;
            parcInserir.DataBaixaPagamento = dataPagamento;
            parcInserir.SituacaoPagamento = (situacaoPagamento ?? parcInserir.SituacaoPagamento);

            HabilitarAlteracaoParcelasAcordo(true);

            await Alterar(parcInserir);

            HabilitarAlteracaoParcelasAcordo(false);
        }

        public async Task AtualizarPagamentoParcelaAcordoBanco(
            decimal parcela, 
            decimal numeroAcordo, 
            DateTime dataPagamento, 
            DateTime dataBaixa, 
            decimal valorPago,
            decimal matricula,
            string motivo,
            int codigoBanco)
        {
            var parcInserir = DbSet.Where(P => P.NumeroAcordo == numeroAcordo
                                            && P.Parcela == parcela).FirstOrDefault();
            var idAluno = _idAlunoRepository.ObterIdAluno(matricula);

            parcInserir.DataPagamento = dataPagamento;
            parcInserir.ValorPago = valorPago;
            parcInserir.DataBaixaPagamento = dataPagamento;
            parcInserir.CodigoBanco = codigoBanco;

            HabilitarAlteracaoParcelasAcordo(true);
            HabilitarAlteracaoAcordoCobrancas(true);

            await Alterar(parcInserir);

            await Db.Database.ExecuteSqlRawAsync(@"insert into scf.obs_reg_pgto(idt_alu, parcela, tpo_pgto, dat_hora, username, texto )
                                values({0}, {1}, 'P', sysdate, sec#_.usuarios_pkg.obter_username, {2});", 
                                idAluno, parcela, motivo);

            await Db.Database.ExecuteSqlRawAsync(@"update SCF.ACORDOS_COBRANCAS set SALDO_DEVEDOR =- {0} WHERE NUM_ACORDO = {1};", 
                                valorPago, numeroAcordo);

            HabilitarAlteracaoAcordoCobrancas(false);
            HabilitarAlteracaoParcelasAcordo(false);
        }

        public decimal? ObterValorParcelaAcordo(decimal parcela, decimal numeroAcordo)
        {
            return DbSet.Where(P => P.Parcela == parcela
                                    && P.NumeroAcordo == numeroAcordo)
                .Select(P => P.Valor).FirstOrDefault();
        }

        public async Task InserirPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, string sistema,
            DateTime dataBaixa, DateTime dataVencimento, decimal valorParcela, string cnpjEmpresaCobranca,
            string tipoInadimplencia)
        {
            HabilitarAlteracaoParcelasAcordo(true);

            await Criar(new ParcelasAcordoModel()
            {
                Parcela = parcela,
                NumeroAcordo = numeroAcordo,
                DataVencimento = dataVencimento,
                DataBaixa = dataBaixa,
                Valor = valorParcela,
                Sistema = sistema,
                CnpjEmpresaCobranca = cnpjEmpresaCobranca,
                TipoInadimplencia = tipoInadimplencia,
            });

            HabilitarAlteracaoParcelasAcordo(false);
        }

        public bool ParcelaPaga(decimal parcela, decimal numeroAcordo)
        {
            return DbSet.Where(P => P.NumeroAcordo == numeroAcordo
                                    && P.Parcela == parcela
                                    && P.DataPagamento != null).Count() > 0;
        }

        public async Task QuitarParcelasAcordo(decimal numeroAcordo, decimal matricula, string sistema, DateTime dataPagamento, decimal periodo, decimal? idTitulo, int? codigoAtividade, int? numeroEvt, decimal? idPessoa, int codigobanco, int codigoAgencia, int numeroConta, decimal numeroCheque, string CpfCnpj)
        {

            var idAluno = _idAlunoRepository.ObterIdAluno(matricula);

            var parcelasTitulo = _parcelaTituloRepository.ObterParcelasPorNumeroAcordo(numeroAcordo);

            foreach (var parcela in parcelasTitulo)
            {
                if (parcela.TipoInadimplencia == "P")
                {
                    if (parcela.Sistema == "S")
                    {
                        var ano = periodo.ToString().Substring(0, 4);
                        var semestre = periodo.ToString().Substring(4, 1);

                        await Db.Database.ExecuteSqlRawAsync(@"update sca.pgto_alunos set sta_pgto = 'R',
                                                                               dat_pgto = '{0}'
                                                                where sta_pgto = 'N'
                                                                  and idt_alu = {1}
                                                                  and ano = {2}
                                                                  and semestre = {3}
                                                                  and parcela = {4}
                                                                  and parcela between 2 and 6
                                                                  and exists ( select 1
                                                                               from scf.itens_geracao ig
                                                                               where ig.matricula = {5}
                                                                                 and ig.periodo = {6}
                                                                                 and ig.parcela = {7} )",
                            dataPagamento, idAluno, ano, semestre, parcela.Parcela, matricula, periodo,
                            parcela.Parcela);


                        await Db.Database.ExecuteSqlRawAsync(@"insert into sca.obs_reg_pgto( ano, semestre, idt_alu, parcela, tpo_pgto, dat_hora, username, texto )
                                                      values( {0}, {1}, {2}, {3}, 'P', sysdate, sec#_.usuarios_pkg.obter_username, Regularização automática através do processamento da baixa da empresa de cobrança' );", periodo.ToString().Substring(0, 4), semestre, idAluno, parcela.Parcela);
                    }
                    else if (parcela.Sistema == "E")
                    {
                        await Db.Database.ExecuteSqlRawAsync(@"update profope.pgto_alunos pgto set pgto.sta_pgto = 'R',
                                                                                                   pgto.dat_pgto = {0}
       	                                                       where sta_pgto = 'N'
             	                                                 and pgto.idt_alu = {1}
                                                                 and pgto.periodo = {2}
                                                                 and pgto.parcela = {3}
                                                                 and pgto.parcela not in ( select min( pgt.parcela ) 
                                                                                             from profope.pgto_alunos pgt 
                                                                                           where pgt.periodo = {2} )
                                                                 and exists ( select 1
                                                                              from scf.itens_geracao ig
                                                                              where ig.matricula = {4}
                                                                                and ig.periodo = {2}
                                                                                and ig.parcela = {3} )", dataPagamento,
                            idAluno, periodo, parcela.Parcela, matricula);


                        await Db.Database.ExecuteSqlRawAsync(
                            @"insert into profope.obs_reg_pgto( idt_alu,parcela,tpo_pgto,dat_hora,username,texto)
      	                                                       values( {0}, {1}, 'P', sysdate, sec#_.usuarios_pkg.obter_username, 'Regularização automática através do  processamento da baixa da empresa de cobrança');",
                            idAluno, parcela.Parcela);
                    }
                    else if (parcela.Sistema == "P")
                    {
                        await Db.Database.ExecuteSqlRawAsync(@"update spgl.pgto_alunos pgto set pgto.sta_pgto = 'R',
                                                                                       pgto.dat_pgto = {0}
                                                               where sta_pgto = 'N'
                                                                 and pgto.idt_alu = {1}
                                                                 and pgto.parcela = {2}
                                                                 and pgto.parcela not in ( select min( pgt.parcela ) 
                                                                                          from spgl.pgto_alunos pgt 
                                                                                          where pgt.periodo = pgto.periodo )
                                                                                            and exists ( select 1
                                                                                                         from scf.itens_geracao ig
                                                                                                         where ig.matricula = {3}
                                                                                                           and ig.periodo  = {4}
                                                                                                           and ig.parcela  = {2} )",
                            dataPagamento, idAluno, parcela.Parcela, matricula, periodo);


                        await Db.Database.ExecuteSqlRawAsync(
                            @"insert into spgl.obs_reg_pgto(idt_alu, parcela, tpo_pgto, dat_hora, username, texto)
                                                               values( {0}, {1}, 'P', sysdate, sec#_.usuarios_pkg.obter_username, 'Regularização automática através do processamento da baixa da empresa de cobrança')",
                            idAluno, parcela.Parcela);
                    }
                    else if (parcela.Sistema == "I")
                    {
                        await Db.Database.ExecuteSqlRawAsync(@"update SCF.v_pgt_titulos pgto set pgto.tpo_baixa = 'R',
                                                                                        pgto.dat_pgto = {0}
                                                               where pgto.dat_pgto is null
                                                                 and pgto.idt_titulo = {1}
                                                                 and pgto.renovacao <= pgto.renov_atual
                                                                 and exists ( select 1
                                                                              from scf.itens_geracao ig
                                                                              where ig.matricula = {2}
                                                                                and ig.periodo  = {3}
                                                                                and ig.parcela  = {4})", dataPagamento,
                            idTitulo, matricula, periodo, parcela.Parcela);


                        await Db.Database.ExecuteSqlRawAsync(
                            @"insert into scf.pgt_obs_reg_tit(idt_titulo,tpo_pgto,dat_hora,username,texto)
                                                               values( {0}, 'P', sysdate, sec#_.usuarios_pkg.obter_username, 'Regularização automática através do processamento da baixa da empresa de cobrança')",
                            idAluno);
                    }
                    else if (parcela.Sistema == "X")
                    {
                        await Db.Database.ExecuteSqlRawAsync(@"update extensao.obs_reg_pgto set pgto.tpo_baixa = 'R',
                                                                                     pgto.dat_pgto = {0}
                                                               where pgto.dat_pgto is null
                                                                 and pgto.cod_atv = {1}
                                                                 and pgto.num_evt = {2}
                                                                 and pgto.idt_ddp = {3}
                                                                 and pgto.num_pc  = {4}
                                                                 and exists ( select 1
                                                                              from scf.itens_geracao ig
                                                                              where ig.matricula = :p_matricula
                                                                                and ig.periodo  = :p_periodo
                                                                                and ig.parcela  = :p_parcela )",
                            dataPagamento, codigoAtividade, numeroEvt, idPessoa, idPessoa);


                        await Db.Database.ExecuteSqlRawAsync(@"insert into extensao.obs_reg_pgto(cod_atv,num_evt,idt_ddp,num_pc,tpo_pgto,dat_hora,username,texto)
                                                               values( {0}, {1}, {2}, {3}, 'P', sysdate, sec#_.usuarios_pkg.obter_username, 'Regularização automática através do processamento da baixa da empresa de cobrança')", codigoAtividade,
                                                                                                                                                                                                                                   numeroEvt,
                                                                                                                                                                                                                                   idPessoa,
                                                                                                                                                                                                                                   parcela.Parcela);
                    }
                }
                else if (parcela.TipoInadimplencia == "T")
                {
                    await Db.Database.ExecuteSqlRawAsync(@"update scf.sap_titulos_avulsos pgto set pgto.tpo_baixa = 'R',
                                                                                        pgto.dat_pgto = {0}
                                                           where pgto.dat_pgto is null
                                                            and pgto.status = 'A'
                                                            and pgto.idt_titulo_avu = {1}
                                                            and exists ( select 1
                                                                         from scf.itens_geracao ig
                                                                         where ig.matricula = {2}
                                                                           and ig.periodo  = {3}
                                                                           and ig.parcela  = {4} )", dataPagamento,
                        idTitulo, matricula, periodo, parcela.Parcela);


                    await Db.Database.ExecuteSqlRawAsync(
                        @"insert into scf.sap_tit_obs_pgto(idt_titulo_avu,username,dat_hora,texto,tpo_pgto)
                                                           values( {0}, sec#_.usuarios_pkg.obter_username, sysdate, 'Regularização automática através do processamento da baixa da empresa de cobrança', 'P')",
                        idTitulo);
                }
                else if (parcela.TipoInadimplencia == "C")
                {
                    await Db.Database.ExecuteSqlRawAsync(@"update scf.cheques pgto  set pgto.dat_reg = {0}
                                                            where pgto.dat_dev is not null
                                                              and pgto.cod_banco   = {1}
                                                              and pgto.cod_agencia = {2}
                                                              and pgto.num_conta   = {3}
                                                              and pgto.num_cheque  = {4}
                                                              and pgto.cpf_cgc     = {5}
                                                              and exists ( select 1
                                                                             from scf.itens_geracao ig
                                                                           where ig.matricula = {6}
                                                                             and ig.periodo  = {7}
                                                                             and ig.parcela  = {8} )", dataPagamento, codigobanco, codigoAgencia, numeroConta, numeroCheque, CpfCnpj, matricula, periodo, parcela.Parcela);


                }
            }

        }

        public async Task InserirObservacaoRegularizacaoParcelaAcordo(long cnpjEmpresaCobranca, decimal numAcordo, decimal parcela, string texto)
        {


            await Db.Database.ExecuteSqlRawAsync(@"insert into scf.obs_reg_pgto_acordo(cnpj_empresa_cobranca,num_acordo,parcela,tpo_pgto,dat_hora,usarname,texto)
                                                           values( {0},{1},{2},'P',sysdate,sec#_.usuarios_pkg.obter_username,{3} )", cnpjEmpresaCobranca, numAcordo, parcela, texto);

        }
    }
}