using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ItensBaixasTipo3Repository : BaseRepository<ItensBaixaTipo3Model>, IitensBaixasTipo3Repository
    {
        public ItensBaixasTipo3Repository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public async Task InserirBaixa(DateTime dataBaixa, decimal matricula, int numeroAcordo, int parcela, DateTime dataPagamento, decimal valorPago, int codErro)
        {
            await Criar(new ItensBaixaTipo3Model() { 
                DataBaixa = dataBaixa,
                Matricula = matricula,
                NumeroAcordo = numeroAcordo,
                Parcela = parcela,
                DataPagamento = dataPagamento,
                ValorPago = valorPago,
                CodigoErro = codErro
            });
        }
    }
}
