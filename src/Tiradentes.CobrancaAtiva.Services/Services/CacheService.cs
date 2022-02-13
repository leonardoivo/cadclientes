using Microsoft.Extensions.DependencyInjection;
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

        private  List<CursoModel> _cursoModel;
        private List<TituloAvulsoModel> _tituloAvulsoModel;
        private List<SituacaoAlunoModel> _situacaoAlunoModel;
        private List<TipoTituloModel> _tipoTituloModel;
        public  List<CursoModel> CursoModel 
        {
            get
            { 
                if(_cursoModel == null)
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ICursoRepository>();
                        _cursoModel = _repository.Buscar().Result.ToList();
                    }
                }

                return _cursoModel;
            }
            set { _cursoModel = value; }
        }
        public  List<TituloAvulsoModel> TituloAvulsoModel
        {
            get
            {
                if (_tituloAvulsoModel == null)
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ITituloAvulsoRepository>();
                        _tituloAvulsoModel = _repository.Buscar().Result.ToList();
                    }
                }

                return _tituloAvulsoModel;
            }
            set { _tituloAvulsoModel = value; }
        }
        public  List<SituacaoAlunoModel> SituacaoAlunoModel
        {
            get
            {
                if (_situacaoAlunoModel == null)
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ISituacaoAlunoRepository>();
                        _situacaoAlunoModel = _repository.Buscar().Result.ToList();
                    }
                }

                return _situacaoAlunoModel;
            }
            set { _situacaoAlunoModel = value; }
        }
        public  List<TipoTituloModel> TipoTituloModel
        {
            get
            {
                if (_tipoTituloModel == null)
                {
                    using (var scope = _scope.CreateScope())
                    {
                        var _repository = scope.ServiceProvider.GetRequiredService<ITipoTituloRepository>();
                        _tipoTituloModel = _repository.Buscar().Result.ToList();
                    }
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
