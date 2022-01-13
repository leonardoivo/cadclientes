using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ItensBaixaTipo3Service : IItensBaixasTipo3Service
    {
        IitensBaixasTipo3Repository _repository;
        public ItensBaixaTipo3Service(IitensBaixasTipo3Repository repository)
        {
            _repository = repository;
        }
        public async Task InserirBaixa(DateTime dataBaixa, decimal matricula, decimal numeroAcordo, int parcela, DateTime dataPagamento, decimal valorPago, decimal? codErro)
        {
            await _repository.InserirBaixa(dataBaixa,
                                           matricula,
                                           numeroAcordo,
                                           parcela,
                                           dataPagamento,
                                           valorPago,
                                           codErro);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
