using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class CacheServiceRepository
    {
        private readonly IServiceScopeFactory _scope;

        private DateTime _timerCursoCache;
        private DateTime _timerTituloAvulsoCache;
        private DateTime _timerSituacaoAlunoCache;
        private DateTime _timerTipoTituloCache;

        private List<CursoModel> _cursoModel;
        private List<TituloAvulsoModel> _tituloAvulsoModel;
        private List<SituacaoAlunoModel> _situacaoAlunoModel;
        private List<TipoTituloModel> _tipoTituloModel;
        public List<CursoModel> CursoModel
        {
            get
            {
                if (_cursoModel == null || (TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalHours - TimeSpan.Parse(_timerCursoCache.ToShortTimeString()).TotalHours > 1))
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ICursoRepository>();
                        _cursoModel = _repository.Buscar().Result.ToList();
                    }

                    _timerCursoCache = DateTime.Now;
                }

                return _cursoModel;
            }
            set { _cursoModel = value; }
        }
        public List<TituloAvulsoModel> TituloAvulsoModel
        {
            get
            {
                if (_tituloAvulsoModel == null || (TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalHours - TimeSpan.Parse(_timerTituloAvulsoCache.ToShortTimeString()).TotalHours > 1))
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ITituloAvulsoRepository>();
                        _tituloAvulsoModel = _repository.Buscar().Result.ToList();
                    }

                    _timerTituloAvulsoCache = DateTime.Now;
                }

                return _tituloAvulsoModel;
            }
            set { _tituloAvulsoModel = value; }
        }
        public List<SituacaoAlunoModel> SituacaoAlunoModel
        {
            get
            {
                if (_situacaoAlunoModel == null || (TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalHours - TimeSpan.Parse(_timerSituacaoAlunoCache.ToShortTimeString()).TotalHours > 1))
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ISituacaoAlunoRepository>();
                        _situacaoAlunoModel = _repository.Buscar().Result.ToList();
                    }

                    _timerSituacaoAlunoCache = DateTime.Now;
                }

                return _situacaoAlunoModel;
            }
            set { _situacaoAlunoModel = value; }
        }
        public List<TipoTituloModel> TipoTituloModel
        {
            get
            {
                if (_tipoTituloModel == null || (TimeSpan.Parse(DateTime.Now.ToShortTimeString()).TotalHours - TimeSpan.Parse(_timerTipoTituloCache.ToShortTimeString()).TotalHours > 1))
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ITipoTituloRepository>();
                        _tipoTituloModel = _repository.Buscar().Result.ToList();
                    }

                    _timerTipoTituloCache = DateTime.Now;
                }

                return _tipoTituloModel;
            }
            set { _tipoTituloModel = value; }
        }

        public CacheServiceRepository(IServiceScopeFactory scopeFactory)
        {
            _scope = scopeFactory;

            _timerCursoCache = DateTime.MinValue;
            _timerSituacaoAlunoCache = DateTime.MinValue;
            _timerTipoTituloCache = DateTime.MinValue;
            _timerTituloAvulsoCache = DateTime.MinValue;

        }
    }
}
