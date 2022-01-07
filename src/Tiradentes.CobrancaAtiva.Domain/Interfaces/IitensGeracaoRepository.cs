using System;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IitensGeracaoRepository : IBaseRepository<ItensGeracaoModel>
    {
        DateTime ObterDataEnvio(decimal cnpjEmpresa, decimal matricula, decimal periodo, int parcela);
        bool ExisteMatricula(decimal cnpjEmpresa, decimal matricula, decimal periodo, int parcela);
    }
}
