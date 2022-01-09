using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IitensBaixasTipo2Repository : IBaseRepository<ItensBaixaTipo2Model>
    {
        Task InserirBaixa(DateTime dataBaixa,
                  decimal matricula,
                  int numeroAcordo,
                  int parcela,
                  int periodo,
                  DateTime dataVencimento,
                  decimal valor,
                  decimal? codErro);
    }
}
