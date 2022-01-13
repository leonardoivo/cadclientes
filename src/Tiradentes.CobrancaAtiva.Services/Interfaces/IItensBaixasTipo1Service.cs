using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IItensBaixasTipo1Service : IDisposable
    {
        Task AtualizarMatricula(DateTime dataBaixa, decimal numeroAcordo, decimal matricula);
        Task InserirBaixa(DateTime dataBaixa,
                  decimal matricula,
                  decimal numeroAcordo,
                  decimal multa,
                  decimal juros,
                  DateTime dataVencimento,
                  decimal valorParcela,
                  decimal? codErro);
    }
}
