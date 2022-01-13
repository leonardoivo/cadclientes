using System;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ArquivoLayoutService : IArquivoLayoutService
    {
        readonly IArquivoLayoutRepository _repository;
        public ArquivoLayoutService(IArquivoLayoutRepository repository)
        {
            _repository = repository;
        }


        public async Task<DateTime> SalvarLayoutArquivo(string status, RespostaViewModel arquivoResposta)
        {
            var layoutArquivo = new ArquivoLayoutModel()
            {
                DataHora = DateTime.Now,
                Conteudo = JsonSerializer.Serialize(arquivoResposta),
                Status = status
            };

            await _repository.Criar(layoutArquivo);

            return layoutArquivo.DataHora;
        }
        public async Task AtualizarStatusLayoutArquivo(DateTime dataHora, string status)
        {
            var model = _repository.BuscarPorDataHora(dataHora);

            model.Status = status;

            await _repository.Alterar(model);
        }
    }
}
