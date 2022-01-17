using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        readonly IMapper _mapper;
        public ErroLayoutService(IErrosLayoutRepository erroLayout,
                                 IArquivoLayoutService arquivoLayoutService,
                                 IMapper mapper)
        {
            _ErroLayout = erroLayout;
            _ArquivoLayout = arquivoLayoutService;
            _mapper = mapper;
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
        public List<ErroLayoutViewModel> BuscarPorDataHora(DateTime dataHora)
        {
            var model = _ErroLayout.BuscarPorDataHora(dataHora);

            return model.Select(E => _mapper.Map<ErroLayoutViewModel>(E)).ToList();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

    }
}
