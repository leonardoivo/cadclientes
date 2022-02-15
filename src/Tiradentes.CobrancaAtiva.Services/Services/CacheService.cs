using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public  class CacheService : ICacheService
    {
        private readonly IServiceScopeFactory _scope;

        private DateTime _UtimaAtualizacaoCurso;
        private DateTime _UtimaAtualizacaoTitulo;
        private DateTime _UtimaAtualizacaoSituacaoAluno;
        private DateTime _UtimaAtualizacaoTipoTitulo;

        private  List<CursoModel> _cursoModel;
        private List<TituloAvulsoModel> _tituloAvulsoModel;
        private List<SituacaoAlunoModel> _situacaoAlunoModel;
        private List<TipoTituloModel> _tipoTituloModel;
        public  List<CursoModel> CursoModel 
        {
            get
            { 
                if(_cursoModel == null || (TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalHours - TimeSpan.Parse(_UtimaAtualizacaoCurso.ToShortTimeString()).TotalHours > 2))
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ICursoRepository>();
                        _cursoModel = _repository.Buscar().Result.ToList();
                    }

                    _UtimaAtualizacaoCurso = DateTime.Now;
                }

                return _cursoModel;
            }
            set { _cursoModel = value; }
        }
        public  List<TituloAvulsoModel> TituloAvulsoModel
        {
            get
            {
                if (_tituloAvulsoModel == null || (TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalHours - TimeSpan.Parse(_UtimaAtualizacaoTitulo.ToShortTimeString()).TotalHours > 2))
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ITituloAvulsoRepository>();
                        _tituloAvulsoModel = _repository.Buscar().Result.ToList();
                    }

                    _UtimaAtualizacaoTitulo = DateTime.Now;
                }

                return _tituloAvulsoModel;
            }
            set { _tituloAvulsoModel = value; }
        }
        public  List<SituacaoAlunoModel> SituacaoAlunoModel
        {
            get
            {
                if (_situacaoAlunoModel == null || (TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalHours - TimeSpan.Parse(_UtimaAtualizacaoSituacaoAluno.ToShortTimeString()).TotalHours > 2))
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ISituacaoAlunoRepository>();
                        _situacaoAlunoModel = _repository.Buscar().Result.ToList();
                    }

                    _UtimaAtualizacaoSituacaoAluno = DateTime.Now;
                }

                return _situacaoAlunoModel;
            }
            set { _situacaoAlunoModel = value; }
        }
        public  List<TipoTituloModel> TipoTituloModel
        {
            get
            {
                if (_tipoTituloModel == null || (TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalHours - TimeSpan.Parse(_UtimaAtualizacaoTipoTitulo.ToShortTimeString()).TotalHours > 2))
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ITipoTituloRepository>();
                        _tipoTituloModel = _repository.Buscar().Result.ToList();
                    }

                    _UtimaAtualizacaoTipoTitulo = DateTime.Now;
                }

                return _tipoTituloModel;
            }
            set { _tipoTituloModel = value; }
        }

        public CacheService(IServiceScopeFactory scopeFactory)
        {
            _scope = scopeFactory;
        }
    }
}
