using System;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IAcordoCobrancaService : IDisposable
    {
        bool ExisteAcordo(decimal numeroAcordo);
        void AtualizarMatriculaAcordo(decimal matricula, decimal numeroAcordo);
    }
}
