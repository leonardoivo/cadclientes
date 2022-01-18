using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Validations.RespostaCobranca;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class CobrancaService : BaseService, ICobrancaService
    {
        protected readonly ICobrancaRepository _repositorio;
        protected readonly IAlunosInadimplentesRepository _alunosInadimplentesRepository;
        protected readonly IMapper _map;

        public CobrancaService(ICobrancaRepository repositorio, IAlunosInadimplentesRepository alunosInadimplentesRepository, IMapper map)
        {
            _repositorio = repositorio;
            _alunosInadimplentesRepository = alunosInadimplentesRepository;
            _map = map;
        }

        public async Task<IEnumerable<RespostaViewModel>> BuscarRepostaNaoIntegrada()
        {
            var arquivosResposta = await _repositorio.Buscar(C => !C.Integrado);

            var viewModelList = from arq in arquivosResposta
                                select _map.Map <RespostaViewModel>(arq);

            return viewModelList;
        }

        public async Task<RespostaViewModel> Criar(RespostaViewModel viewModel)
        {
            Validate(new CriarRespostaCobrancaValidation(), viewModel);


            var model = _map.Map<RespostasCollection>(viewModel);

            await _repositorio.Criar(model);

            return _map.Map<RespostaViewModel>(model);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

        }

    }
}
