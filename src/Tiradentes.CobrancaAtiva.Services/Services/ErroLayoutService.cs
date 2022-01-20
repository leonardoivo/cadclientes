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
        readonly IMapper _mapper;
        public ErroLayoutService(IErrosLayoutRepository erroLayout,                                 
                                 IMapper mapper)
        {
            _ErroLayout = erroLayout;            
            _mapper = mapper;
        }

        public List<ErroLayoutViewModel> BuscarPorDataHora(DateTime dataHora)
        {
            var model = _ErroLayout.BuscarPorDataHora(dataHora);

            if (model == null)
                return new List<ErroLayoutViewModel>();

            return model.Select(E => _mapper.Map<ErroLayoutViewModel>(E)).ToList();
        }

        public async Task<decimal?> CriarErroLayoutService(DateTime dataHora, ErrosBaixaPagamento erro, string descricao)
        {

            var model = new ErrosLayoutModel
            {
                DataHora = dataHora,

            };

            _ErroLayout.HabilitarAlteracaoErroLayout(true);

            await _ErroLayout.Criar(model);


            _ErroLayout.HabilitarAlteracaoErroLayout(false);

            return model.Sequencia;

            ////var model = new ErrosLayoutModel
            ////{
            ////    DataHora = dataHora,
            ////    Descricao = string.IsNullOrEmpty(descricao) ? Application.Utils.Utils.GetDescricaoEnum(erro) : Application.Utils.Utils.GetDescricaoEnum(erro) + " => " + descricao

            ////};

            //_ErroLayout.HabilitarAlteracaoErroLayout(true);

            ////await _ErroLayout.Criar(model);

            //await _ErroLayout.CriarErrosLayout(dataHora, string.IsNullOrEmpty(descricao) ? Application.Utils.Utils.GetDescricaoEnum(erro) : Application.Utils.Utils.GetDescricaoEnum(erro) + " => " + descricao);

            //_ErroLayout.HabilitarAlteracaoErroLayout(false);

            //return _ErroLayout.BuscarPorDataHora(dataHora).LastOrDefault().Sequencia;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ErroLayout?.Dispose();
            }
        }

    }
}
