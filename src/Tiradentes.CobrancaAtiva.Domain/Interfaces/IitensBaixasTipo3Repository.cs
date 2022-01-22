using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IItensBaixasTipo3Repository : IBaseRepository<ItensBaixaTipo3Model>
    {
        Task InserirBaixa(DateTime dataBaixa, Int64 matricula, Int64 numeroAcordo, int parcela, DateTime dataPagamento, decimal valorPago, int codErro, string cnpjEmpresaCobranca, string sistema, string situacaoAluno, string tipoInadimplencia, string tipo_Pagamento);
    }
}
