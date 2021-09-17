using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class HonorarioEmpresaParceiraService : IHonorarioEmpresaParceiraService
    {
        protected readonly IHonorarioEmpresaParceiraRepository _repositorio;
        protected readonly IMapper _map;

        public HonorarioEmpresaParceiraService(IHonorarioEmpresaParceiraRepository repositorio, IMapper map)
        {
            _repositorio = repositorio;
            _map = map;
        }

        public void Dispose()
        {
            _repositorio?.Dispose();
        }
    }
}
