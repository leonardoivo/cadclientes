﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ItensBaixasTipo1Repository : BaseRepository<ItensBaixaTipo1Model>, IItensBaixasTipo1Repository
    {
        public ItensBaixasTipo1Repository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public async Task AtualizarMatricula(DateTime dataBaixa, decimal numeroAcordo, decimal matricula)
        {
            var itemBaixa = DbSet.Where(I => I.DataBaixa == dataBaixa
                                        && I.NumeroAcordo == numeroAcordo).FirstOrDefault();

            itemBaixa.Matricula = matricula;

            await Alterar(itemBaixa);
        }

        public async Task InserirBaixa(DateTime dataBaixa, decimal matricula, decimal numeroAcordo, decimal multa, decimal juros, DateTime dataVencimento, decimal valorParcela, decimal? codErro, string cnpjEmpresaCobranca, int parcela, string sistema, string situacaoAluno, string tipoInadimplencia)
        {
            HabilitarAlteracaoBaixaCobranca(true);

            await Criar(new ItensBaixaTipo1Model(){ 
                DataBaixa = dataBaixa,
                Matricula = matricula,
                NumeroAcordo = numeroAcordo,
                Multa = multa,
                Juros = juros,
                DataVencimento = dataVencimento,
                Valor = valorParcela,
                CodigoErro = codErro,
                CnpjEmpresaCobranca = cnpjEmpresaCobranca,
                Parcela = parcela,
                Sistema = sistema,
                SituacaoAluno = situacaoAluno,
                TipoInadimplencia = tipoInadimplencia             
            });

            HabilitarAlteracaoBaixaCobranca(false);
        }

        private  void HabilitarAlteracaoBaixaCobranca(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_it_bx_tp1({status.ToString().ToLower()});
                                         end;");
        }

    }
}
