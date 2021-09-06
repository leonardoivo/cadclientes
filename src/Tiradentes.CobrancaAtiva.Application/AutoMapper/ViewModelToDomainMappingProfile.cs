using AutoMapper;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EmpresaParceiraViewModel, EmpresaParceiraModel>()
                .ForSourceMember(dest => dest.CEP, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.Estado, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.Cidade, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.Logradouro, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.Numero, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.Complemento, opt => opt.DoNotValidate());
            CreateMap<ContatoEmpresaParceiraViewModel, ContatoEmpresaParceiraModel>();
            CreateMap<EnderecoEmpresaParceiraViewModel, EnderecoEmpresaParceiraModel>();
        }
    }
}
