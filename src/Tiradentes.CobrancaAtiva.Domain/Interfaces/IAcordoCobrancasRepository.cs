using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IAcordoCobrancasRepository : IBaseRepository<AcordosCobrancasModel>
    {
        bool ExisteAcordo(decimal numeroAcordo);
        Task AtualizarMatriculaAcordo(decimal matricula, decimal numeroAcordo);
        Task AtualizarSaldoDevedor(decimal numeroAcordo, decimal valor);
        Task InserirAcordoCobranca(decimal numeroAcordo, DateTime dataBaixa, DateTime dataAcordo, int totalParcelas, decimal valorTotal, decimal multa, decimal juros, decimal matricula, decimal saldoDevedor, string cpf, string cnpjEmpresaCobranca, string sistema, string tipoInadimplencia);

        decimal ObterMatricula(decimal numeroAcordo);


    }
}
