using AutoMapper;
using System.Linq;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Endereco;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Semestre;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
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
                    opt => opt.MapFrom(src => src.Contatos.FirstOrDefault().Contato))
                .ForMember(dest => dest.Cidade, 
                    opt => opt.MapFrom(src => src.Endereco.Cidade))
                .ForMember(dest => dest.Estado, 
                    opt => opt.MapFrom(src => src.Endereco.Estado));
            CreateMap<EmpresaParceiraModel, EmpresaParceiraViewModel>()
                .ForMember(dest => dest.CEP, opt => opt.MapFrom(src => src.Endereco.CEP))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Endereco.Estado))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Endereco.Cidade))
                .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Endereco.Logradouro))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Endereco.Numero))
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Endereco.Complemento));
            CreateMap<ModelPaginada<EmpresaParceiraModel>, ViewModelPaginada<BuscaEmpresaParceiraViewModel>>();
            CreateMap<ModelPaginada<HonorarioEmpresaParceiraModel>, ViewModelPaginada<HonorarioEmpresaParceiraViewModel>>();
            CreateMap<ContatoEmpresaParceiraModel, ContatoEmpresaParceiraViewModel>();
            CreateMap<EnderecoEmpresaParceiraModel, EnderecoEmpresaParceiraViewModel>();
            CreateMap<HonorarioEmpresaParceiraModel, HonorarioEmpresaParceiraViewModel>();
            CreateMap<InstituicaoModalidadeRegraModel, InstituicaoModalidadeRegraViewModel>();

            CreateMap<InstituicaoModel, InstituicaoViewModel>();
            CreateMap<ModalidadeModel, ModalidadeViewModel>();

            CreateMap<CursoModel, CursoViewModel>();
            CreateMap<SemestreModel, SemestreViewModel>();
            CreateMap<SituacaoModel, SituacaoViewModel>();
            CreateMap<TipoPagamentoModel, TipoPagamentoViewModel>();
            CreateMap<TipoTituloModel, TipoTituloViewModel>();


            CreateMap<EnderecoModel, EnderecoViewModel>();
        }
    }
}
