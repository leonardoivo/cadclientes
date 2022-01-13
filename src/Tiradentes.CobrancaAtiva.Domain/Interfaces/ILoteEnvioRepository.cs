using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Collections;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface ILoteEnvioRepository
    {
        Task<LoteEnvioCollection> GetLoteEnvio(string numeroLote);
    }
}
