using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class ParametroEnvioModel : BaseModel
    {
        public ParametroEnvioModel()
        { }

        public int InstituicaoId { get; private set; }
        public int ModalidadeId { get; private set; }
        public int EmpresaParceiraId { get; private set; }
        public int DiaEnvio { get; private set; }
        public bool Status { get; set; }
        public DateTime InadimplenciaInicial { get; private set; }
        public DateTime InadimplenciaFinal { get; private set; }
        public DateTime ValidadeInicial { get; private set; }
        public DateTime ValidadeFinal { get; private set; }

        public InstituicaoModel Instituicao { get; set; }
        public ModalidadeModel Modalidade { get; set; }
        public EmpresaParceiraModel EmpresaParceira { get; set; }

        public ICollection<ParametroEnvioCursoModel> ParametroEnvioCurso { get; private set; }
        public ICollection<ParametroEnvioSituacaoAlunoModel> ParametroEnvioSituacaoAluno { get; private set; }
        public ICollection<ParametroEnvioTipoTituloModel> ParametroEnvioTipoTitulo { get; private set; }
        public ICollection<ParametroEnvioTituloAvulsoModel> ParametroEnvioTituloAvulso { get; private set; }
        
        
        public void SetParametroEnvioCurso(ICollection<ParametroEnvioCursoModel> datas ) 
        {
            ParametroEnvioCurso = ParametroEnvioCurso
                                    .Select(r => new ParametroEnvioCursoModel {
                                        Id = datas.FirstOrDefault(c => c.CursoId.Equals(r.CursoId))?.Id ?? 0,
                                        ParametroEnvioId = Id,
                                        CursoId = r.CursoId
                                    })
                                    .ToList();
        }

         public void SetParametroEnvioTituloAvulso(ICollection<ParametroEnvioTituloAvulsoModel> datas ) 
        {
            ParametroEnvioTituloAvulso = ParametroEnvioTituloAvulso
                                    .Select(r => new ParametroEnvioTituloAvulsoModel {
                                        Id = datas.FirstOrDefault(c => c.TituloAvulsoId.Equals(r.TituloAvulsoId))?.Id ?? 0,
                                        ParametroEnvioId = Id,
                                        TituloAvulsoId = r.TituloAvulsoId
                                    })
                                    .ToList();
        }

        public void SetParametroEnvioSituacaoAluno(ICollection<ParametroEnvioSituacaoAlunoModel> datas ) 
        {
            ParametroEnvioSituacaoAluno = ParametroEnvioSituacaoAluno
                                    .Select(r => new ParametroEnvioSituacaoAlunoModel {
                                        Id = datas.FirstOrDefault(c => c.SituacaoAlunoId.Equals(r.SituacaoAlunoId))?.Id ?? 0,
                                        ParametroEnvioId = Id,
                                        SituacaoAlunoId = r.SituacaoAlunoId
                                    })
                                    .ToList();
        }

         public void SetParametroEnvioTipoTitulo(ICollection<ParametroEnvioTipoTituloModel> datas ) 
        {
            ParametroEnvioTipoTitulo = ParametroEnvioTipoTitulo
                                    .Select(r => new ParametroEnvioTipoTituloModel {
                                        Id = datas.FirstOrDefault(c => c.TipoTituloId.Equals(r.TipoTituloId))?.Id ?? 0,
                                        ParametroEnvioId = Id,
                                        TipoTituloId = r.TipoTituloId
                                    })
                                    .ToList();
        }
    }
}