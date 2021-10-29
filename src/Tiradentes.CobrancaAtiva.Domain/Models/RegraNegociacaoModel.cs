using System;
using System.Collections.Generic;
using System.Linq;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class RegraNegociacaoModel : BaseModel
    {
        public RegraNegociacaoModel()
        { }

        public int InstituicaoId { get; private set; }
        public int ModalidadeId { get; private set; }
        public bool Status { get; set; }
        public DateTime MesAnoInicial { get; private set; }
        public DateTime MesAnoFinal { get; private set; }
        public DateTime ValidadeInicial { get; private set; }
        public DateTime ValidadeFinal { get; private set; }

        public decimal PercentJurosMultaAVista { get; private set; }
        public decimal PercentValorAVista { get; private set; }

        public decimal PercentJurosMultaCartao { get; private set; }
        public decimal PercentValorCartao { get; private set; }
        public int QuantidadeParcelasCartao { get; private set; }

        public decimal PercentJurosMultaBoleto { get; private set; }
        public decimal PercentValorBoleto { get; private set; }
        public decimal PercentEntradaBoleto { get; private set; }
        public int QuantidadeParcelasBoleto { get; private set; }


        public InstituicaoModel Instituicao { get; set; }
        public ModalidadeModel Modalidade { get; set; }
        public ICollection<RegraNegociacaoCursoModel> RegraNegociacaoCurso { get; private set; }
        public ICollection<RegraNegociacaoTituloAvulsoModel> RegraNegociacaoTituloAvulso { get; private set; }
        public ICollection<RegraNegociacaoSituacaoAlunoModel> RegraNegociacaoSituacaoAluno { get; private set; }
        public ICollection<RegraNegociacaoTipoTituloModel> RegraNegociacaoTipoTitulo { get; private set; }

        public void SetRegraNegociacaoCurso(ICollection<RegraNegociacaoCursoModel> datas ) 
        {
            RegraNegociacaoCurso = RegraNegociacaoCurso
                                    .Select(r => new RegraNegociacaoCursoModel {
                                        Id = datas.FirstOrDefault(c => c.CursoId.Equals(r.CursoId))?.Id ?? 0,
                                        RegraNegociacaoId = Id,
                                        CursoId = r.CursoId
                                    })
                                    .ToList();
        }

         public void SetRegraNegociacaoTituloAvulso(ICollection<RegraNegociacaoTituloAvulsoModel> datas ) 
        {
            RegraNegociacaoTituloAvulso = RegraNegociacaoTituloAvulso
                                    .Select(r => new RegraNegociacaoTituloAvulsoModel {
                                        Id = datas.FirstOrDefault(c => c.TituloAvulsoId.Equals(r.TituloAvulsoId))?.Id ?? 0,
                                        RegraNegociacaoId = Id,
                                        TituloAvulsoId = r.TituloAvulsoId
                                    })
                                    .ToList();
        }

        public void SetRegraNegociacaoSituacaoAluno(ICollection<RegraNegociacaoSituacaoAlunoModel> datas ) 
        {
            RegraNegociacaoSituacaoAluno = RegraNegociacaoSituacaoAluno
                                    .Select(r => new RegraNegociacaoSituacaoAlunoModel {
                                        Id = datas.FirstOrDefault(c => c.SituacaoAlunoId.Equals(r.SituacaoAlunoId))?.Id ?? 0,
                                        RegraNegociacaoId = Id,
                                        SituacaoAlunoId = r.SituacaoAlunoId
                                    })
                                    .ToList();
        }

         public void SetRegraRegraNegociacaoTipoTitulo(ICollection<RegraNegociacaoTipoTituloModel> datas ) 
        {
            RegraNegociacaoTipoTitulo = RegraNegociacaoTipoTitulo
                                    .Select(r => new RegraNegociacaoTipoTituloModel {
                                        Id = datas.FirstOrDefault(c => c.TipoTituloId.Equals(r.TipoTituloId))?.Id ?? 0,
                                        RegraNegociacaoId = Id,
                                        TipoTituloId = r.TipoTituloId
                                    })
                                    .ToList();
        }
    }
}
