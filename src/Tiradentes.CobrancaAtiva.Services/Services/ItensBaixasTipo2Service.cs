using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ItensBaixasTipo2Service : IItensBaixasTipo2Service
    {
        readonly IitensBaixasTipo2Repository _repository;
        public ItensBaixasTipo2Service(IitensBaixasTipo2Repository repository)
        {
            _repository = repository;
        }
        public async Task InserirBaixa(DateTime dataBaixa, decimal matricula, int numeroAcordo, int parcela, int periodo, DateTime dataVencimento, decimal valor, int codErro)
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
            throw new NotImplementedException();
        }
    }
}
