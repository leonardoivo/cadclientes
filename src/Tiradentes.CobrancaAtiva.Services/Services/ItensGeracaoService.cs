using System;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ItensGeracaoService : IItensGeracaoService
    {
        readonly IItensGeracaoRepository _repository;
        public ItensGeracaoService(IItensGeracaoRepository repository)
        {
            _repository = repository;
        }

        public DateTime ObterDataEnvio(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela)
        {
            return _repository.ObterDataEnvio(cnpjEmpresa,
                                              matricula,
                                              periodo,
                                              parcela);
        }

        public bool ExisteMatricula(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela)
        {
            return _repository.ExisteMatricula(cnpjEmpresa,
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
