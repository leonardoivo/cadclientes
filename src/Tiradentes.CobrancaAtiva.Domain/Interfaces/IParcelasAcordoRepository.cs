using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IParcelasAcordoRepository : IBaseRepository<ParcelasAcordoModel>
    {
        bool ExisteParcelaAcordo(decimal parcela, decimal numeroAcordo);
        bool ExisteParcelaPaga(decimal numeroAcordo);
        bool ParcelaPaga(decimal parcela, decimal numeroAcordo);
        Task QuitarParcelasAcordo(decimal numeroAcordo, decimal matricula, string sistema, DateTime dataPagamento, decimal periodo, decimal? idTitulo, int? codigoAtividade, int? numeroEvt, decimal? idPessoa, int codigobanco, int codigoAgencia, int numeroConta, decimal numeroCheque, string CpfCnpj);
        Task InserirObservacaoRegularizacaoParcelaAcordo(long cnpjEmpresaCobranca, decimal numAcordo, decimal parcela, string texto);
        decimal? ObterValorParcelaAcordo(decimal parcela, decimal numeroAcordo);
        Task AtualizarPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, DateTime dataPagamento, DateTime dataBaixa, decimal valorPago, string situacaoPagamento);
        Task InserirPagamentoParcelaAcordo(decimal parcela, decimal numeroAcordo, string sistema, DateTime dataBaixa, DateTime dataVencimento, decimal valorParcela, string cnpjEmpresaCobranca, string tipoInadimplencia);
        Task EstornarParcelaAcordo(decimal parcela, decimal numeroAcordo);
        Task AtualizarPagamentoParcelaAcordoBanco(
            decimal parcela, 
            decimal numeroAcordo, 
            DateTime dataPagamento, 
            DateTime dataBaixa, 
            decimal valorPago,
            decimal matricula,
            string motivo,
            int codigoBanco);
    }
}