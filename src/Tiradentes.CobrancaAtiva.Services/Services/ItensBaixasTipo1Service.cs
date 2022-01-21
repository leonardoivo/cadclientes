using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ItensBaixasTipo1Service : IItensBaixasTipo1Service
    {
        readonly IItensBaixasTipo1Repository _repository;
        public ItensBaixasTipo1Service(IItensBaixasTipo1Repository repository)
        {
            _repository = repository;
        }
        public async Task AtualizarMatricula(DateTime dataBaixa, Int64 numeroAcordo, Int64 matricula)
        {
            await _repository.AtualizarMatricula(dataBaixa,
                                                 numeroAcordo,
                                                 matricula);
        }

        public async Task InserirBaixa(DateTime dataBaixa, Int64 matricula, Int64 numeroAcordo, decimal multa, decimal juros, DateTime dataVencimento, decimal valorParcela, int codErro, string cnpjEmpresaCobranca, int parcela, string sistema, string situacaoAluno, string tipoInadimplencia)
        {
            await _repository.InserirBaixa(dataBaixa,
                                           matricula,
                                           numeroAcordo,
                                           multa,
                                           juros,
                                           dataVencimento,
                                           valorParcela,
                                           codErro,
                                           cnpjEmpresaCobranca,
                                           parcela,
                                           sistema,
                                           situacaoAluno,
                                           tipoInadimplencia);
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
