using System;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IItensGeracaoService : IDisposable
    {
        DateTime ObterDataEnvio(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela, string periodoOutros);
        bool ExisteMatricula(string cnpjEmpresa, decimal matricula, decimal periodo, int parcela, string periodoOutros);
    }
}
