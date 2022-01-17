using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ErrosLayoutModel> BuscarPorDataHora(DateTime dataHora)
        {
            return DbSet.Where(E => E.DataHora == dataHora).ToList();
        }
    }
}
