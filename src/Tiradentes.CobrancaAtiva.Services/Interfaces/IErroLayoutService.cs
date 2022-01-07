using System;
using Tiradentes.CobrancaAtiva.Domain.Enum;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IErroLayoutService : IDisposable
    {
        decimal RegistrarErro(ErrosBaixaPagamento erro);
    }
}
