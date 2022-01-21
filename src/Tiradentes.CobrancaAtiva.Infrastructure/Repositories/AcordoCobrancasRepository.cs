using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class AcordoCobrancasRepository : BaseRepository<AcordosCobrancasModel>, IAcordoCobrancasRepository
    {
        public AcordoCobrancasRepository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public async Task AtualizarMatriculaAcordo(decimal matricula, decimal numeroAcordo)
        {
            
            var acordo = DbSet.Where(A => A.NumeroAcordo == numeroAcordo && A.Matricula == null).FirstOrDefault();

            acordo.Matricula = matricula;

            await Alterar(acordo);
            

        }

        public bool ExisteAcordo(decimal numeroAcordo)
        {
            return DbSet.Where(A => A.NumeroAcordo == numeroAcordo).Count() > 0;
        }

        public async Task AtualizarSaldoDevedor(decimal numeroAcordo, decimal valor)
        {
            var atualizarParcela = DbSet.Where(P => P.NumeroAcordo == numeroAcordo).FirstOrDefault();

            atualizarParcela.SaldoDevedor += valor;

            await Alterar(atualizarParcela);

        }

        public async Task InserirAcordoCobranca(decimal numeroAcordo, DateTime dataBaixa, DateTime dataAcordo, int totalParcelas, decimal valorTotal, decimal multa, decimal juros, decimal matricula, decimal saldoDevedor, string cpf, string cnpjEmpresaCobranca, string sistema, string tipoInadimplencia)
        {

            HabilitarAlteracaoBaixaCobranca(true);

           await Criar(new AcordosCobrancasModel(){
                    NumeroAcordo = numeroAcordo,
                    DataBaixa = dataBaixa,
                    Data = dataAcordo,
                    TotalParcelas = totalParcelas,
                    ValorTotal = valorTotal,
                    Multa = multa,
                    Matricula = matricula,
                    SaldoDevedor = saldoDevedor,
                    CPF = cpf,
                    CnpjEmpresaCobranca = cnpjEmpresaCobranca,
                    Sistema = sistema,
                    TipoInadimplencia = tipoInadimplencia,
                    Mora = juros
           });

            HabilitarAlteracaoBaixaCobranca(false);
        }

        public decimal ObterMatricula(decimal numeroAcordo)
        {
            return DbSet.Where(A => A.NumeroAcordo == numeroAcordo).Select(A => A.NumeroAcordo).FirstOrDefault();
        }

        private void HabilitarAlteracaoBaixaCobranca(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_acord_cob({status.ToString().ToLower()});
                                         end;
                                         ");
        }
    }
}
