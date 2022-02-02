﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class ParcelaTituloRepository : BaseRepository<ParcelasTitulosModel>, IParcelaTituloRepository
    {
        public ParcelaTituloRepository(CobrancaAtivaDbContext context) : base(context)
        {

        }

        public bool ExisteParcela(decimal matricula, decimal periodo, int parcela, string periodoOutros)
        {
            return DbSet.Where(P => P.Matricula == matricula
                                 && P.Periodo == periodo
                                 && P.PeriodoOutros == periodoOutros
                                 && P.Parcela == parcela).Count() > 0;
        }

        public bool ExisteParcelaInadimplente(DateTime dataBaixa)
        {
            return DbSet.Where(P => P.DataBaixa.Date == dataBaixa.Date).Count() > 0;
        }

        public async Task InserirParcela(Int64 numeroAcordo, Int64 matricula, decimal periodo, int parcela, DateTime dataBaixa, DateTime dataEnvio, DateTime dataVencimento, decimal valorParcela, string cnpjEmpresaCobranca, string sistema, string tipoInadimplencia, string periodoOutros)
        {

            HabilitarAlteracaoParcelaTitulo(true);

            await Criar(new ParcelasTitulosModel()
            {
                NumeroAcordo = numeroAcordo,
                Matricula = matricula,
                Periodo = periodo,
                Parcela = parcela,
                DataBaixa = dataBaixa,
                DataEnvio = dataEnvio,
                DataVencimento = dataVencimento,
                Valor = valorParcela,
                CnpjEmpresaCobranca = cnpjEmpresaCobranca,
                Sistema = sistema,
                TipoInadimplencia = tipoInadimplencia,
                PeriodoOutros = periodoOutros
            });

            HabilitarAlteracaoParcelaTitulo(false);
        }

        public IEnumerable<ParcelasTitulosModel> ObterParcelasPorNumeroAcordo(decimal numeroAcordo)
        {
            return DbSet.Where(P => P.NumeroAcordo == numeroAcordo).AsEnumerable();
        }

        private void HabilitarAlteracaoParcelaTitulo(bool status)
        {
            Db.Database.ExecuteSqlRaw($@"begin
                                         scf.COBRANCAS_PKG.set_pode_alt_parc_tit({status.ToString().ToLower()});
                                         end;");
        }
    }
}
