using System;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IItensGeracaoService : IDisposable
    {
        DateTime ObterDataEnvio(decimal cnpjEmpresa, decimal matricula, decimal periodo, int parcela);
        bool ExisteMatricula(decimal cnpjEmpresa, decimal matricula, decimal periodo, int parcela);
    }
}
