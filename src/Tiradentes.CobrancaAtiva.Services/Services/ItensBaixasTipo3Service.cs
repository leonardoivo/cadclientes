using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ItensBaixasTipo3Service : IItensBaixasTipo3Service
    {
        readonly IItensBaixasTipo3Repository _repository;
        public ItensBaixasTipo3Service(IItensBaixasTipo3Repository repository)
        {
            _repository = repository;
        }
        public async Task InserirBaixa(DateTime dataBaixa, Int64 matricula, Int64 numeroAcordo, int parcela, DateTime dataPagamento, decimal valorPago, int codErro, string cnpjEmpresaCobranca, string sistema, string situacaoAluno, string tipoInadimplencia, string tipo_Pagamento)
        {
            await _repository.InserirBaixa(dataBaixa,
                                           matricula,
                                           numeroAcordo,
                                           parcela,
                                           dataPagamento,
                                           valorPago,
                                           codErro,
                                           cnpjEmpresaCobranca,
                                           sistema,
                                           situacaoAluno,
                                           tipoInadimplencia,
                                           tipo_Pagamento);
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
                _repository?.Dispose();
            }
        }

    }
}
