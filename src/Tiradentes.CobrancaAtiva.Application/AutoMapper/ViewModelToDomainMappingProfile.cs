using AutoMapper;
using System.Linq;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.ParametroEnvio;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
using Tiradentes.CobrancaAtiva.Domain.Collections;
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
                .ForSourceMember(dest => dest.Complemento, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.BancoId, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.ContaCorrente, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.CodigoAgencia, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.Convenio, opt => opt.DoNotValidate())
                .ForSourceMember(dest => dest.Pix, opt => opt.DoNotValidate());
            CreateMap<ContatoEmpresaParceiraViewModel, ContatoEmpresaParceiraModel>();
            CreateMap<EnderecoEmpresaParceiraViewModel, EnderecoEmpresaParceiraModel>();
            CreateMap<ContaBancariaEmpresaParceiraViewModel, ContaBancariaEmpresaParceiraModel>();

            CreateMap<HonorarioEmpresaParceiraViewModel, HonorarioEmpresaParceiraModel>();
            CreateMap<CreateHonorarioEmpresaParceiraViewModel, HonorarioEmpresaParceiraModel>();
            CreateMap<CreateHonorarioFaixaEmpresaParceiraViewModel, HonorarioFaixaEmpresaParceiraModel>();

            CreateMap<CriarRegraNegociacaoViewModel, RegraNegociacaoModel>()
                .ForMember(dest => dest.RegraNegociacaoCurso,
                    opt => opt.MapFrom(src => src.CursoIds.Select(c => new RegraNegociacaoCursoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoTituloAvulso,
                    opt => opt.MapFrom(src => src.TitulosAvulsosId.Select(c => new RegraNegociacaoTituloAvulsoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoSituacaoAluno,
                    opt => opt.MapFrom(src => src.SituacaoAlunoIds.Select(c => new RegraNegociacaoSituacaoAlunoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoTipoTitulo,
                    opt => opt.MapFrom(src => src.TipoTituloIds.Select(c => new RegraNegociacaoTipoTituloModel(c))));
            CreateMap<AlterarRegraNegociacaoViewModel, RegraNegociacaoModel>()
                .ForMember(dest => dest.RegraNegociacaoCurso,
                    opt => opt.MapFrom(src => src.CursoIds.Select(c => new RegraNegociacaoCursoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoTituloAvulso,
                    opt => opt.MapFrom(src => src.TitulosAvulsosId.Select(c => new RegraNegociacaoTituloAvulsoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoSituacaoAluno,
                    opt => opt.MapFrom(src => src.SituacaoAlunoIds.Select(c => new RegraNegociacaoSituacaoAlunoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoTipoTitulo,
                    opt => opt.MapFrom(src => src.TipoTituloIds.Select(c => new RegraNegociacaoTipoTituloModel(c))));

            CreateMap<CriarParametroEnvioViewModel, ParametroEnvioModel>()
                .ForMember(dest => dest.ParametroEnvioCurso,
                    opt => opt.MapFrom(src => src.CursoIds.Select(c => new ParametroEnvioCursoModel(c))))
                .ForMember(dest => dest.ParametroEnvioTituloAvulso,
                    opt => opt.MapFrom(src => src.TituloAvulsoIds.Select(c => new ParametroEnvioTituloAvulsoModel(c))))
                .ForMember(dest => dest.ParametroEnvioSituacaoAluno,
                    opt => opt.MapFrom(src => src.SituacaoAlunoIds.Select(c => new ParametroEnvioSituacaoAlunoModel(c))))
                .ForMember(dest => dest.ParametroEnvioTipoTitulo,
                    opt => opt.MapFrom(src => src.TipoTituloIds.Select(c => new ParametroEnvioTipoTituloModel(c))));
            CreateMap<AlterarParametroEnvioViewModel, ParametroEnvioModel>()
                .ForMember(dest => dest.ParametroEnvioCurso,
                    opt => opt.MapFrom(src => src.CursoIds.Select(c => new ParametroEnvioCursoModel(c))))
                .ForMember(dest => dest.ParametroEnvioTituloAvulso,
                    opt => opt.MapFrom(src => src.TituloAvulsoIds.Select(c => new ParametroEnvioTituloAvulsoModel(c))))
                .ForMember(dest => dest.ParametroEnvioSituacaoAluno,
                    opt => opt.MapFrom(src => src.SituacaoAlunoIds.Select(c => new ParametroEnvioSituacaoAlunoModel(c))))
                .ForMember(dest => dest.ParametroEnvioTipoTitulo,
                    opt => opt.MapFrom(src => src.TipoTituloIds.Select(c => new ParametroEnvioTipoTituloModel(c))));

            CreateMap<ConflitoViewModel, ConflitoModel>();
            CreateMap<ConflitoDetalheViewModel, ConflitoDetalheModel>();

            CreateMap<RespostasCollection, RespostaViewModel>().ReverseMap();

            CreateMap<BaixasCobrancasModel, BaixasCobrancasViewModel>().ReverseMap();
            CreateMap<ArquivoLayoutModel, ArquivoLayoutViewModel>();
            CreateMap<ErrosLayoutModel, ErroLayoutViewModel>();

            CreateMap<CriarRespostaViewModel, RespostasCollection>();
        }
    }
}
