using System;
using System.Linq;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ItensGeracaoRepository : BaseRepository<ItensGeracaoModel>, IitensGeracaoRepository
    {
        public ItensGeracaoRepository(CobrancaAtivaDbContext context): base(context)
        {

        }

        public bool ExisteMatricula(decimal cnpjEmpresa, decimal matricula, decimal periodo, int parcela)
        {
            return DbSet.Where(I => I.CnpjEmpresaCobranca == cnpjEmpresa
                                 && I.Matricula == matricula
                                 && I.Periodo == periodo
                                 && I.Parcela == parcela).Count() > 0;
        }

        public DateTime ObterDataEnvio(decimal cnpjEmpresa, decimal matricula, decimal periodo, int parcela)
        {
            return DbSet.Where(I => I.CnpjEmpresaCobranca == cnpjEmpresa
                                 && I.Matricula == matricula
                                 && I.Periodo == periodo
                                 && I.Parcela == parcela)
                        .Select(I => I.DataGeracao).FirstOrDefault();
        }
    }
}
