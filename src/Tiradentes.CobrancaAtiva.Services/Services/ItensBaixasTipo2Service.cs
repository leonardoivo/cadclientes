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
        public async Task InserirBaixa(DateTime dataBaixa, decimal matricula, decimal numeroAcordo, int parcela, decimal periodo, DateTime dataVencimento, decimal valor, int? codErro)
        {
            await _repository.InserirBaixa(dataBaixa,
                                           matricula,
                                           numeroAcordo,
                                           parcela,
                                           periodo,
                                           dataVencimento,
                                           valor,
                                           codErro);
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
