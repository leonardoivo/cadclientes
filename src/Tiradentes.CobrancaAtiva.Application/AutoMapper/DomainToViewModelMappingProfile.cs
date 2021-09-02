using AutoMapper;
using System.Linq;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<EmpresaParceiraModel, BuscaEmpresaParceiraViewModel>()
                .ForMember(dest => dest.Contato, 
                    opt => opt.MapFrom(src => src.Contatos.FirstOrDefault().Contato));
            CreateMap<EmpresaParceiraModel, EmpresaParceiraViewModel>();
            CreateMap<ContatoModel, ContatoViewModel>();
        }
    }
}
