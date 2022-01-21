using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ItensBaixasTipo2Repository : BaseRepository<ItensBaixaTipo2Model>, IItensBaixasTipo2Repository
    {
        public ItensBaixasTipo2Repository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public async Task InserirBaixa(DateTime dataBaixa, decimal matricula, decimal numeroAcordo, int parcela, decimal periodo, DateTime dataVencimento, decimal valor, int codErro)
        {

            HabilitarAlteracaoBaixaCobranca(true);

            await Criar(new ItensBaixaTipo2Model() { 
                DataBaixa = dataBaixa,
                Matricula = matricula,
                NumeroAcordo = numeroAcordo,
                Parcela = parcela,
                Periodo = periodo,
                DataVencimento = dataVencimento,
                Valor = valor,
                CodigoErro = codErro
            });

            HabilitarAlteracaoBaixaCobranca(false);
        }

        private void HabilitarAlteracaoBaixaCobranca(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_it_bx_tp2({status.ToString().ToLower()});
                                         end;");
        }

    }
}
