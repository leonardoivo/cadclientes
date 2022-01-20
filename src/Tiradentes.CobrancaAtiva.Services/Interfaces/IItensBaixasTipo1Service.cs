using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IItensBaixasTipo1Service : IDisposable
    {
        Task AtualizarMatricula(DateTime dataBaixa, Int64 numeroAcordo, Int64 matricula);
        Task InserirBaixa(DateTime dataBaixa, Int64 matricula, Int64 numeroAcordo, decimal multa, decimal juros, DateTime dataVencimento, decimal valorParcela, int? codErro, string cnpjEmpresaCobranca, int parcela, string sistema, string situacaoAluno, string tipoInadimplencia);
    }
}
