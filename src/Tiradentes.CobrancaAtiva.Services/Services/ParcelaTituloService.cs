﻿using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ParcelaTituloService : IParcelaTituloService
    {
        IParcelaTituloRepository _parcelaTituloRepository;
        public ParcelaTituloService(IParcelaTituloRepository parcelaTituloRepository)
        {
            _parcelaTituloRepository = parcelaTituloRepository;
        }
        public bool ExisteParcela(decimal matricula, decimal periodo, int parcela)
        {
            return _parcelaTituloRepository.ExisteParcela(matricula, periodo, parcela);
        }

        public async Task InserirParcela(decimal numeroAcordo, decimal matricula, decimal periodo, int parcela, DateTime dataBaixa, DateTime dataEnvio, DateTime dataVencimento, decimal valorParcela)
        {
            await _parcelaTituloRepository.InserirParcela(numeroAcordo,
                                                          matricula,
                                                          periodo,
                                                          parcela,
                                                          dataBaixa,
                                                          dataEnvio,
                                                          dataVencimento,
                                                          valorParcela);
        }
        public bool ExisteParcelaInadimplente(DateTime dataBaixa)
        {
            return _parcelaTituloRepository.ExisteParcelaInadimplente(dataBaixa);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
