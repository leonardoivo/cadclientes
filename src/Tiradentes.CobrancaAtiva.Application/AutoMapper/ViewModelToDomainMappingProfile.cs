﻿using AutoMapper;
using System.Linq;
using Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.HonorarioEmpresaParceira;
using Tiradentes.CobrancaAtiva.Application.ViewModels.RegraNegociacao;
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
            
            CreateMap<HonorarioEmpresaParceiraViewModel, HonorarioEmpresaParceiraModel>();
            CreateMap<CreateHonorarioEmpresaParceiraViewModel, HonorarioEmpresaParceiraModel>(); 
            
            CreateMap<CriarRegraNegociacaoViewModel, RegraNegociacaoModel>()
                .ForMember(dest => dest.RegraNegociacaoCurso, 
                    opt => opt.MapFrom(src => src.CursoIds.Select(c => new RegraNegociacaoCursoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoSemestre,
                    opt => opt.MapFrom(src => src.SemestreIds.Select(c => new RegraNegociacaoSemestreModel(c))))
                .ForMember(dest => dest.RegraNegociacaoSituacaoAluno,
                    opt => opt.MapFrom(src => src.SituacaoAlunoIds.Select(c => new RegraNegociacaoSituacaoAlunoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoTipoPagamento,
                    opt => opt.MapFrom(src => src.TipoPagamentoIds.Select(c => new RegraNegociacaoTipoPagamentoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoTipoTitulo,
                    opt => opt.MapFrom(src => src.TipoTituloIds.Select(c => new RegraNegociacaoTipoTituloModel(c))));
            CreateMap<AlterarRegraNegociacaoViewModel, RegraNegociacaoModel>()
                .ForMember(dest => dest.RegraNegociacaoCurso, 
                    opt => opt.MapFrom(src => src.CursoIds.Select(c => new RegraNegociacaoCursoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoSemestre,
                    opt => opt.MapFrom(src => src.SemestreIds.Select(c => new RegraNegociacaoSemestreModel(c))))
                .ForMember(dest => dest.RegraNegociacaoSituacaoAluno,
                    opt => opt.MapFrom(src => src.SituacaoAlunoIds.Select(c => new RegraNegociacaoSituacaoAlunoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoTipoPagamento,
                    opt => opt.MapFrom(src => src.TipoPagamentoIds.Select(c => new RegraNegociacaoTipoPagamentoModel(c))))
                .ForMember(dest => dest.RegraNegociacaoTipoTitulo,
                    opt => opt.MapFrom(src => src.TipoTituloIds.Select(c => new RegraNegociacaoTipoTituloModel(c))));  
        }
    }
}
