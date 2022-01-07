using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IitensBaixasTipo1Repository : IBaseRepository<ItensBaixaTipo1Model>
    {
        Task AtualizarMatricula(DateTime dataBaixa, decimal numeroAcordo, decimal matricula );
        Task InserirBaixa(DateTime dataBaixa,
                  decimal matricula,
                  int numeroAcordo,
                  decimal multa,
                  decimal juros,
                  DateTime dataVencimento,
                  decimal valorParcela,
                  int codErro);
    }
}
