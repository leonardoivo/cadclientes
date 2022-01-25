using System;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IItensGeracaoService : IDisposable
    {
        DateTime ObterDataEnvio(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela);
        bool ExisteMatricula(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela);
    }
}
