using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ParcelasAcordoService : IParcelasAcordoService
    {
        readonly IParcelasAcordoRepository _parcelasAcordoRepository;
        public ParcelasAcordoService(IParcelasAcordoRepository parcelasAcordoRepository)
        {
            _parcelasAcordoRepository = parcelasAcordoRepository;
        }

        public async Task AtualizaPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataPagamento, DateTime dataBaixa, decimal valorPago)
        {
            await _parcelasAcordoRepository.AtualizaPagamentoParcelaAcordo(parcela,
                                                                           numeroAcordo,
                                                                           dataPagamento,
                                                                           dataBaixa,
                                                                           valorPago);
        }


        public async Task EstornarParcelaAcordo(decimal parcela, decimal numeroAcordo)
        {
            await _parcelasAcordoRepository.EstornarParcelaAcordo(parcela, numeroAcordo);
        }

        public bool ExisteParcelaAcordo(decimal parcela, decimal numeroAcordo)
        {
            return _parcelasAcordoRepository.ExisteParcelaAcordo(parcela, numeroAcordo);
        }

        public bool ExisteParcelaPaga(decimal numeroAcordo)
        {
            return _parcelasAcordoRepository.ExisteParcelaPaga(numeroAcordo);
        }

        public decimal? ObterValorParcelaAcordo(decimal parcela, decimal numeroAcordo)
        {
            return _parcelasAcordoRepository.ObterValorParcelaAcordo(parcela, numeroAcordo);
        }

        public async Task QuitarParcelasAcordo(decimal numeroAcordo)
        {
            await _parcelasAcordoRepository.QuitarParcelasAcordo(numeroAcordo);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task InserirPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataVencimento, DateTime dataBaixa, decimal valorPago)
        {
            await _parcelasAcordoRepository.InserirPagamentoParcelaAcordo(parcela,
                                                                          numeroAcordo,
                                                                          dataVencimento,
                                                                          dataBaixa,
                                                                          valorPago);
        }
    }
}
