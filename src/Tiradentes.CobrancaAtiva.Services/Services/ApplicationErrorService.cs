using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.TituloAvulso;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ApplicationErrorService : IApplicationErrorService
    {
        protected readonly IApplicationErrorRepository _repositorio;
        protected readonly IMapper _map;

        public ApplicationErrorService(IApplicationErrorRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public async Task LogError(ApplicationErrorCollection model)
        {
            await _repositorio.Insert(model);
        }

        public void Dispose() { }
    }
}
