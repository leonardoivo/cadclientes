using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface ICacheService
    {
        static List<CursoModel> CursoModel { get; set; }
        static List<TituloAvulsoModel> TituloAvulsoModel { get; set; }
        static List<SituacaoAlunoModel> SituacaoAlunoModel { get; set; }
        static List<TipoTituloModel> TipoTituloModel { get; set; }

    }
}
