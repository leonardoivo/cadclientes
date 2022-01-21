using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IParcelaTituloRepository : IBaseRepository<ParcelasTitulosModel>
    {
        bool ExisteParcela(decimal matricula, decimal periodo, int parcela);
        bool ExisteParcelaInadimplente(DateTime dataBaixa);
        Task InserirParcela(Int64 numeroAcordo, Int64 matricula, decimal periodo, int parcela, DateTime dataBaixa, DateTime dataEnvio, DateTime dataVencimento, decimal valorParcela);

        IEnumerable<ParcelasTitulosModel> ObterParcelasPorNumeroAcordo(decimal numeroAcordo);
    }
}
