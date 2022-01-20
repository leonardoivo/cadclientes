using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IItensBaixasTipo2Service : IDisposable
    {
        Task InserirBaixa(DateTime dataBaixa, decimal matricula, decimal numeroAcordo, int parcela, decimal periodo, DateTime dataVencimento, decimal valor, int? codErro);
    }
}
