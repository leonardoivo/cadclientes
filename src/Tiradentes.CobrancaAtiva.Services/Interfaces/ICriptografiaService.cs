using System;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ICriptografiaService : IDisposable
    {
        Task<string> Criptografar(string dado);
        Task<string> Descriptografar(string dado);
    }
}