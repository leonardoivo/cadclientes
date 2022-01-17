using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class AcordoCobrancaService : IAcordoCobrancaService
    {
        readonly IAcordoCobrancasRepository _repository;
        public AcordoCobrancaService(IAcordoCobrancasRepository Repository)
        {
            _repository = Repository;
        }
        public async Task AtualizarMatriculaAcordo(decimal matricula, decimal numeroAcordo)
        {

           await _repository.AtualizarMatriculaAcordo(matricula, numeroAcordo);

         
        }

        public bool ExisteAcordo(decimal numeroAcordo)
        {            
           return _repository.ExisteAcordo(numeroAcordo);
        }


        public Task InserirAcordoCobranca(decimal numeroAcordo, DateTime dataBaixa, DateTime dataAcordo, int totalParcelas, decimal valorTotal, decimal multa, decimal matricula, decimal saldoDevedor)
        {
            throw new NotImplementedException();
        }

        public async Task AtualizarSaldoDevedor(decimal numeroAcordo, decimal valor)
        {
            await _repository.AtualizarSaldoDevedor(numeroAcordo, valor);
        }

        public decimal ObterMatricula(decimal numeroAcordo)
        {
            return _repository.ObterMatricula(numeroAcordo);
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
