using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Enum;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ErroLayoutService : IErroLayoutService
    {
        readonly IErrosLayoutRepository _ErroLayout;
        readonly IArquivoLayoutService _ArquivoLayout;
        public ErroLayoutService(IErrosLayoutRepository erroLayout,
                                 IArquivoLayoutService arquivoLayout)
        {
            _ErroLayout = erroLayout;
            _ArquivoLayout = arquivoLayout;
        }
        public async Task<decimal?> RegistrarErro(ErrosBaixaPagamento erro, RespostaViewModel conteudo)
        {
            var dateTime = await _ArquivoLayout.SalvarLayoutArquivo("E", conteudo);

            var erroModel = new ErrosLayoutModel() {

                DataHora = dateTime,
                Descricao = Application.Utils.Utils.GetDescricaoEnum(erro)
            };

            await _ErroLayout.Criar(erroModel);

            return erroModel.Sequencia;
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

    }
}
