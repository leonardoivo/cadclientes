using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IItensBaixasTipo1Repository : IBaseRepository<ItensBaixaTipo1Model>
    {
        Task AtualizarMatricula(DateTime dataBaixa, Int64 numeroAcordo, Int64 matricula );
        Task InserirBaixa(DateTime dataBaixa, Int64 matricula, Int64 numeroAcordo, decimal multa, decimal juros, DateTime dataVencimento, decimal valorParcela, int? codErro, string cnpjEmpresaCobranca, int parcela, string sistema, string situacaoAluno, string tipoInadimplencia);
    }
}
