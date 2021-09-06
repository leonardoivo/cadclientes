using AutoMapper;
using System.Linq;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Domain.DTO;
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
            CreateMap<EmpresaParceiraModel, EmpresaParceiraViewModel>()
                .ForMember(dest => dest.CEP, opt => opt.MapFrom(src => src.Endereco.CEP))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Endereco.Estado))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Endereco.Cidade))
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Endereco.Logradouro))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Endereco.Numero))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Endereco.Complemento));
            CreateMap<ModelPaginada<EmpresaParceiraModel>, ViewModelPaginada<BuscaEmpresaParceiraViewModel>>();
            CreateMap<ContatoEmpresaParceiraModel, ContatoEmpresaParceiraViewModel>();
            CreateMap<EnderecoEmpresaParceiraModel, EnderecoEmpresaParceiraViewModel>();

            CreateMap<InstituicaoModel, InstituicaoViewModel>();
        }
    }
}
