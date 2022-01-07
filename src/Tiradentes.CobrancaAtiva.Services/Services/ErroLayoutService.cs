using Tiradentes.CobrancaAtiva.Domain.Enum;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ErroLayoutService : IErroLayoutService
    {
        readonly IErrosLayoutRepository _ErroLayout;
        readonly IArquivoLayoutRepository _ArquivoLayout;
        public ErroLayoutService(IErrosLayoutRepository erroLayout, IArquivoLayoutRepository arquivoLayout)
        {
            _ErroLayout = erroLayout;
            _ArquivoLayout = arquivoLayout;
        }
        public decimal RegistrarErro(ErrosBaixaPagamento erro)
        {
            throw new System.NotImplementedException();
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

    }
}
