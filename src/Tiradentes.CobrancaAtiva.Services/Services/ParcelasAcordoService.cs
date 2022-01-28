using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.BaixaPagamento;
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

        public async Task AtualizaPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataPagamento, DateTime dataBaixa, decimal valorPago, char? situacaoPagamento)
        {
            await _parcelasAcordoRepository.AtualizarPagamentoParcelaAcordo(parcela,
                                                                           numeroAcordo,
                                                                           dataPagamento,
                                                                           dataBaixa,
                                                                           valorPago,
                                                                           situacaoPagamento);
        }

        public async Task AtualizaPagamentoParcelaAcordoBanco(BaixaPagamentoParcelaManualViewModel viewModel)
        {
            await _parcelasAcordoRepository.AtualizarPagamentoParcelaAcordoBanco(viewModel.Parcela,
                viewModel.NumeroAcordo,
                viewModel.DataBaixa,
                DateTime.Now,
                viewModel.ValorBaixa,
                viewModel.Matricula,
                viewModel.Motivo,
                viewModel.CodigoBanco);
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

        public async Task QuitarParcelasAcordo(decimal numeroAcordo, decimal matricula, string sistema, DateTime dataPagamento, decimal periodo, decimal? idTitulo, int? codigoAtividade, int? numeroEvt, decimal? idPessoa, int codigobanco, int codigoAgencia, int numeroConta, decimal numeroCheque, string CpfCnpj)
        {
            await _parcelasAcordoRepository.QuitarParcelasAcordo(numeroAcordo, matricula, sistema, dataPagamento, periodo, idTitulo, codigoAtividade, numeroEvt, idPessoa, codigobanco, codigoAgencia, numeroConta, numeroCheque, CpfCnpj);
        }
        public async Task InserirPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, string sistema, DateTime dataBaixa, DateTime dataVencimento, decimal valorParcela, string cnpjEmpresaCobranca, string tipoInadimplencia)
        {
            await _parcelasAcordoRepository.InserirPagamentoParcelaAcordo(parcela,
                                                                          numeroAcordo,
                                                                          sistema,
                                                                          dataBaixa,
                                                                          dataVencimento,
                                                                          valorParcela,
                                                                          cnpjEmpresaCobranca,
                                                                          tipoInadimplencia);
        }

        public async Task InserirObservacaoRegularizacaoParcela(string tipoInadimplencia, string parcela, decimal matricula, string sistema, decimal periodo, decimal? idTitulo, int? codigoAtividade, int? numeroEvt, decimal? idPessoa, string texto)
        {
            await _parcelasAcordoRepository.InserirObservacaoRegularizacaoParcela(tipoInadimplencia, parcela, matricula, sistema, periodo, idTitulo, codigoAtividade, numeroEvt, idPessoa, texto);
        }

        public bool ParcelaPaga(decimal parcela, decimal numeroAcordo)
        {
            return _parcelasAcordoRepository.ParcelaPaga(parcela, numeroAcordo);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _parcelasAcordoRepository?.Dispose();
            }
        }

    }
}
