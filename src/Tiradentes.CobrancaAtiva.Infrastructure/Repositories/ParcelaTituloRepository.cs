using System;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ParcelaTituloRepository : BaseRepository<ParcelasTitulosModel>, IParcelaTituloRepository
    {
        public ParcelaTituloRepository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public bool ExisteParcela(decimal matricula, decimal periodo, int parcela)
        {
            return DbSet.Where(P => P.Matricula == matricula
                                 && P.Periodo == periodo
                                 && P.Parcela == parcela).Count() > 0;
        }

        public async Task InserirParcela(decimal numeroAcordo, decimal matricula, decimal periodo, int parcela, DateTime dataBaixa, DateTime dataEnvio, DateTime dataVencimento, decimal valorParcela)
        {
           await  Criar(new ParcelasTitulosModel(){
                            NumeroAcordo = numeroAcordo,
                            Matricula = matricula,
                            Periodo = periodo,
                            Parcela = parcela,
                            DataBaixa = dataBaixa,
                            DataEnvio = dataEnvio,
                            DataVencimento = dataVencimento,
                            Valor = valorParcela
                        });
        }
    }
}
