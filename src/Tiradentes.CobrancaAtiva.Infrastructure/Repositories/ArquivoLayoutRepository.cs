using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ArquivoLayoutRepository : BaseRepository<ArquivoLayoutModel>, IArquivoLayoutRepository
    {
        public ArquivoLayoutRepository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public ArquivoLayoutModel BuscarPorDataHora(DateTime dataHora)
        {
            return DbSet.FirstOrDefault(A => A.DataHora.Year == dataHora.Year
                                          && A.DataHora.Month == dataHora.Month
                                          && A.DataHora.Day == dataHora.Day
                                          && A.DataHora.Hour == dataHora.Hour
                                          && A.DataHora.Minute == dataHora.Minute);
        }

        public void HabilitarAlteracaoArquivoLayout(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_arq_layout({status.ToString().ToLower()});
                                         end;");
        }
    }
}
