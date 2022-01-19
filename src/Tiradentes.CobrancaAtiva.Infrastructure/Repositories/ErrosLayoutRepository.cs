using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Enum;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ErrosLayoutRepository : BaseRepository<ErrosLayoutModel>, IErrosLayoutRepository
    {
        public ErrosLayoutRepository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public async Task CriarErrosLayout(DateTime dataHora, ErrosBaixaPagamento erro, string descricao)
        {
            await Db.Database.ExecuteSqlRawAsync($@"insert into scf.erros_layout( dat_hora, dsc_erro )
                                                    values(p_dat_hora, p_dsc_erro); ");
        }

        public List<ErrosLayoutModel> BuscarPorDataHora(DateTime dataHora)
        {
            return DbSet.Where(E => E.DataHora == dataHora).ToList();
        }
        

        public void HabilitarAlteracaoErroLayout(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_erros_layout({status.ToString().ToLower()});
                                         end;");
        }
    }
}
