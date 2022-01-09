using System;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ParcelasAcordoRepository : BaseRepository<ParcelasAcordoModel>, IParcelasAcordoRepository
    {
        public ParcelasAcordoRepository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public async Task EstornarParcelaAcordo(decimal parcela, decimal numeroAcordo)
        {
            var parcAcordo = DbSet.Where(P => P.Parcela == parcela
                                           && P.NumeroAcordo == numeroAcordo).FirstOrDefault();

            parcAcordo.DataPagamento = null;
            parcAcordo.DataBaixaPagamento = null;
            parcAcordo.ValorPago = null;

            await Alterar(parcAcordo);

        }

        public bool ExisteParcelaAcordo(decimal parcela, decimal numeroAcordo)
        {
            return this.DbSet.Where(P => P.Parcela == parcela
                                    && P.NumeroAcordo == numeroAcordo).Count() > 1;
        }

        public bool ExisteParcelaPaga(decimal numeroAcordo)
        {
            return DbSet.Where(P => P.NumeroAcordo == numeroAcordo
                                 && P.DataPagamento != null
                                 && P.ValorPago != null).Count() > 0;
        }

        public async Task AtualizarPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataPagamento, DateTime dataBaixa, decimal valorPago)
        {
            var parcInserir = DbSet.Where(P => P.NumeroAcordo == numeroAcordo
                                            && P.Parcela == parcela).FirstOrDefault();

            parcInserir.DataPagamento = dataPagamento;
            parcInserir.ValorPago = valorPago;
            parcInserir.DataBaixaPagamento = dataPagamento;

            await Alterar(parcInserir);

        }

        public decimal? ObterValorParcelaAcordo(decimal parcela, decimal numeroAcordo)
        {
            return DbSet.Where(P => P.Parcela == parcela
                          && P.NumeroAcordo == numeroAcordo)
                        .Select(P => P.Valor).FirstOrDefault();
        }

        public Task QuitarParcelasAcordo(decimal numeroAcordo)
        {
            // PROCESSANDO TIPO 2 K
            throw new NotImplementedException();
        }

        public async Task InserirPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataVencimento, DateTime dataBaixa, decimal valorPago)
        {
            await Criar(new ParcelasAcordoModel(){
                Parcela = parcela,
                NumeroAcordo = numeroAcordo,
                DataVencimento = dataVencimento,
                DataBaixa = dataBaixa,
                ValorPago = valorPago
            });
        }

        public bool ParcelaPaga(decimal parcela, decimal numeroAcordo)
        {
            return DbSet.Where(P => P.NumeroAcordo == numeroAcordo
                                 && P.Parcela == parcela
                                 && P.DataPagamento != null).Count() > 0;
        }
    }
}
