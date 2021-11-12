using System;
using System.Collections.Generic;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.DTO
{
    public class BuscaParametroEnvio 
    {
        public int Id { get; set; }
        public EmpresaParceiraModel EmpresaParceira { get; set; }
        public bool Status { get; set; }
        public int DiaEnvio { get; set; }
        public DateTime InadimplenciaInicial { get; set; }
        public DateTime InadimplenciaFinal { get; set; }
        public DateTime ValidadeInicial { get; set; }
        public DateTime ValidadeFinal { get; set; }

        public IEnumerable<InstituicaoModel> Instituicoes { get; set; }
        public IEnumerable<ModalidadeModel> Modalidades { get; set; }
        public IEnumerable<CursoModel> Cursos { get; set; }
        public IEnumerable<TituloAvulsoModel> TitulosAvulsos { get; set; }
        public IEnumerable<SituacaoAlunoModel> SituacoesAlunos { get; set; }
        public IEnumerable<TipoTituloModel> TiposTitulos { get; set; }
    }
}