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

        public DateTime ObterDataEnvio(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela, string periodoOutros)
        {
            return _repository.ObterDataEnvio(cnpjEmpresa,
                                              matricula,
                                              periodo,
                                              parcela,
                                              periodoOutros);
        }

        public bool ExisteMatricula(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela, string periodoOutros)
        {
            return _repository.ExisteMatricula(cnpjEmpresa,
                                                 matricula,
                                                 periodo,
                                                 parcela,
                                                 periodoOutros);
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
