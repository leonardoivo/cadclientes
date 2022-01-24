﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ModalidadeRepository : BaseRepository<ModalidadeModel>, IModalidadeRepository
    {
        public ModalidadeRepository(CobrancaAtivaDbContext context) : base(context)
        { }

        public async Task<IList<ModalidadeModel>> BuscarPorInstituicao(int instituicaoId)
        {
            return await DbSet
                .Where(m => m.InstituicoesModalidades
                    .Where(im => im.InstituicaoId == instituicaoId)
                    .Any())
                .ToListAsync();
        }

        public async Task<ModalidadeModel> BuscarPorCodigo(string codigo)
        {
            return await DbSet
                .Where(i => i.CodigoMagister == codigo)
                .FirstAsync();
        }
    }
}
