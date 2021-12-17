using AutoMapper;
using System.Linq;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Banco;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Curso;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Endereco;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Instituicao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Modalidade;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Semestre;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Situacao;
using Tiradentes.CobrancaAtiva.Application.ViewModels.SituacaoAluno;
using Tiradentes.CobrancaAtiva.Application.ViewModels.TituloAvulso;
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
                .ForMember(dest => dest.Complemento, opt => opt.MapFrom(src => src.Endereco.Complemento))
                .ForMember(dest => dest.BancoId, opt => opt.MapFrom(src => src.ContaBancaria.BancoId))
                .ForMember(dest => dest.ContaCorrente, opt => opt.MapFrom(src => src.ContaBancaria.ContaCorrente))
                .ForMember(dest => dest.CodigoAgencia, opt => opt.MapFrom(src => src.ContaBancaria.CodigoAgencia))
                .ForMember(dest => dest.Convenio, opt => opt.MapFrom(src => src.ContaBancaria.Convenio))
                .ForMember(dest => dest.Pix, opt => opt.MapFrom(src => src.ContaBancaria.Pix));
            CreateMap<ModelPaginada<EmpresaParceiraModel>, ViewModelPaginada<BuscaEmpresaParceiraViewModel>>();
            CreateMap<ModelPaginada<HonorarioEmpresaParceiraModel>, ViewModelPaginada<HonorarioEmpresaParceiraViewModel>>();
            CreateMap<ModelPaginada<RegraNegociacaoModel>, ViewModelPaginada<RegraNegociacaoViewModel>>();
            CreateMap<ModelPaginada<ParametroEnvioModel>, ViewModelPaginada<ParametroEnvioViewModel>>();
            CreateMap<ModelPaginada<BuscaRegraNegociacao>, ViewModelPaginada<BuscaRegraNegociacaoViewModel>>();
            CreateMap<ModelPaginada<BuscaParametroEnvio>, ViewModelPaginada<BuscaParametroEnvioViewModel>>();
            CreateMap<ContatoEmpresaParceiraModel, ContatoEmpresaParceiraViewModel>();
            CreateMap<EnderecoEmpresaParceiraModel, EnderecoEmpresaParceiraViewModel>();
            CreateMap<ContaBancariaEmpresaParceiraModel, ContaBancariaEmpresaParceiraViewModel>();
            CreateMap<HonorarioEmpresaParceiraModel, HonorarioEmpresaParceiraViewModel>();

            CreateMap<InstituicaoModel, InstituicaoViewModel>();
            CreateMap<ModalidadeModel, ModalidadeViewModel>();

            CreateMap<CursoModel, CursoViewModel>();
            CreateMap<SemestreModel, SemestreViewModel>();
            CreateMap<SituacaoAlunoModel, SituacaoAlunoViewModel>();
            CreateMap<TipoPagamentoModel, TipoPagamentoViewModel>();
            CreateMap<TipoTituloModel, TipoTituloViewModel>();
            CreateMap<TituloAvulsoModel, TituloAvulsoViewModel>();
            
            CreateMap<EnderecoModel, EnderecoViewModel>();
            CreateMap<BancoModel, BancoViewModel>();

            CreateMap<RegraNegociacaoModel, RegraNegociacaoViewModel>();
            CreateMap<BuscaRegraNegociacao, BuscaRegraNegociacaoViewModel>();

            CreateMap<ParametroEnvioModel, ParametroEnvioViewModel>();
            CreateMap<BuscaParametroEnvio, BuscaParametroEnvioViewModel>();
            
            CreateMap<ConflitoModel, ConflitoViewModel>();
        }
    }
}

