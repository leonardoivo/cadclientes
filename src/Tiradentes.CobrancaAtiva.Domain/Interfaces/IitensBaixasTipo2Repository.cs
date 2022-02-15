using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IItensBaixasTipo2Repository : IBaseRepository<ItensBaixaTipo2Model>
    {
        Task InserirBaixa(DateTime dataBaixa, Int64 matricula, Int64 numeroAcordo, int parcela, decimal periodo, DateTime dataVencimento, decimal valor, int codErro, string cnpjEmpresaCobranca, string sistema, string situacaoAluno, string tipoInadimplencia, string periodoOutros);
    }
}