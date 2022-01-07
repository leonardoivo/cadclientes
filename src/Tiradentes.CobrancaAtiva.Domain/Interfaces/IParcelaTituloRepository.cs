using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IParcelaTituloRepository : IBaseRepository<ParcelasTitulosModel>
    {
        bool ExisteParcela(decimal matricula, decimal periodo, int parcela);
        Task InserirParcela(decimal numeroAcordo, decimal matricula, decimal periodo, int parcela, DateTime dataBaixa, DateTime dataEnvio, DateTime dataVencimento, decimal valorParcela);
    }
}
