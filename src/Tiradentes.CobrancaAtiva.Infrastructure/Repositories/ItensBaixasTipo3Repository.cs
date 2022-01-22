using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ItensBaixasTipo3Repository : BaseRepository<ItensBaixaTipo3Model>, IItensBaixasTipo3Repository
    {
        public ItensBaixasTipo3Repository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public async Task InserirBaixa(DateTime dataBaixa, Int64 matricula, Int64 numeroAcordo, int parcela, DateTime dataPagamento, decimal valorPago, int codErro, string cnpjEmpresaCobranca, string sistema, string situacaoAluno, string tipoInadimplencia, string tipo_Pagamento)
        {
            HabilitarAlteracaoBaixaCobranca(true);

            await Criar(new ItensBaixaTipo3Model() { 
                DataBaixa = dataBaixa.Date,
                Matricula = matricula,
                NumeroAcordo = numeroAcordo,
                Parcela = parcela,
                DataPagamento = dataPagamento,
                ValorPago = valorPago,
                CodigoErro = codErro,
                CnpjEmpresaCobranca = cnpjEmpresaCobranca,
                Sistema = sistema,
                SituacaoAluno = situacaoAluno,
                TipoInadimplencia = tipoInadimplencia,
                Tipo_Pagamento = tipo_Pagamento,
                NumeroLinha = 3
            });

            HabilitarAlteracaoBaixaCobranca(false);
        }

        private void HabilitarAlteracaoBaixaCobranca(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_it_bx_tp3({status.ToString().ToLower()});
                                         end;");
        }
    }
}
