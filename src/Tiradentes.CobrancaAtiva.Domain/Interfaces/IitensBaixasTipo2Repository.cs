using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IItensBaixasTipo2Repository : IBaseRepository<ItensBaixaTipo2Model>
    {
        Task InserirBaixa(DateTime dataBaixa, decimal matricula, decimal numeroAcordo, int parcela, decimal periodo, DateTime dataVencimento, decimal valor, decimal? codErro);
    }
}
