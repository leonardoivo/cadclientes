using System;
using System.Collections.Generic;
using System.Linq;
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
        public void AtualizarMatriculaAcordo(decimal matricula, decimal numeroAcordo)
        {

           _repository.AtualizarMatriculaAcordo(matricula, numeroAcordo);

         
        }

        public bool ExisteAcordo(decimal numeroAcordo)
        {            
           return _repository.ExisteAcordo(numeroAcordo);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
