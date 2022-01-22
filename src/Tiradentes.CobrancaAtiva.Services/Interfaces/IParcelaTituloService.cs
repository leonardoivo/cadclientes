using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IParcelaTituloService : IDisposable
    {
        bool ExisteParcela(decimal matricula, decimal periodo, int parcela);
        bool ExisteParcelaInadimplente(DateTime dataBaixa);
        Task InserirParcela(Int64 numeroAcordo, Int64 matricula, decimal periodo, int parcela, DateTime dataBaixa, DateTime dataEnvio, DateTime dataVencimento, decimal valorParcela, string cnpjEmpresaCobranca, string sistema, string tipoInadimplencia, decimal periodoChequeDevolvido);
    }
}
