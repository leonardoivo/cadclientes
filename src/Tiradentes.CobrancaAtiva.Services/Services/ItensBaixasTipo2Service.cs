using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ItensBaixasTipo2Service : IItensBaixasTipo2Service
    {
        readonly IItensBaixasTipo2Repository _repository;
        public ItensBaixasTipo2Service(IItensBaixasTipo2Repository repository)
        {
            _repository = repository;
        }              
        public async Task InserirBaixa(DateTime dataBaixa, Int64 matricula, Int64 numeroAcordo, int parcela, decimal periodo, DateTime dataVencimento, decimal valor, int codErro, string cnpjEmpresaCobranca, string sistema, string situacaoAluno, string tipoInadimplencia, int? periodoChequeDevolvido)
        {
            await _repository.InserirBaixa(dataBaixa,
                                           matricula,
                                           numeroAcordo,
                                           parcela,
                                           periodo,
                                           dataVencimento,
                                           valor,
                                           codErro,
                                           cnpjEmpresaCobranca,
                                           sistema,
                                           situacaoAluno,
                                           tipoInadimplencia,
                                           periodoChequeDevolvido);
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
