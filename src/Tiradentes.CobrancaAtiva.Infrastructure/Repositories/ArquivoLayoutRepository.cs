using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CriarArquivoLayout(ArquivoLayoutModel model)
        {
            DbSet.Local.Clear();
            await Criar(model);

        }
        public List<ArquivoLayoutModel> BuscarPorDataHora(DateTime dataHora)
        {
            return DbSet.Where(A => A.DataHora.Year == dataHora.Year
                                          && A.DataHora.Month == dataHora.Month
                                          && A.DataHora.Day == dataHora.Day
                                          && A.DataHora.Hour == dataHora.Hour
                                          && A.DataHora.Minute == dataHora.Minute
                                          && A.DataHora.Second == dataHora.Second).ToList();
        }

        public ArquivoLayoutModel BuscarLayoutSucessoPorData(DateTime dataHora)
        {
            return DbSet.FirstOrDefault(A => A.DataHora.Year == dataHora.Year
                                          && A.DataHora.Month == dataHora.Month
                                          && A.DataHora.Day == dataHora.Day
                                          && A.Status == "S");
        }

        public void HabilitarAlteracaoArquivoLayout(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_arq_layout({status.ToString().ToLower()});
                                         end;");
        }
    }
}
