using System;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ItensGeracaoService : IItensGeracaoService
    {
        IitensGeracaoRepository _repository;
        public ItensGeracaoService(IitensGeracaoRepository repository)
        {
            _repository = repository;
        }
        public bool ExisteMatricula(decimal cnpjEmpresa, decimal matricula, decimal periodo, int parcela)
        {
           return _repository.ExisteMatricula(cnpjEmpresa,
                                                matricula,
                                                periodo,
                                                parcela);
        }

        public DateTime ObterDataEnvio(decimal cnpjEmpresa, decimal matricula, decimal periodo, int parcela)
        {
            return _repository.ObterDataEnvio(cnpjEmpresa,
                                              matricula,
                                              periodo,
                                              parcela);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
