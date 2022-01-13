﻿using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class ItemBaixaTipo1Service : IItensBaixasTipo1Service
    {
        readonly IitensBaixasTipo1Repository _repository;
        public ItemBaixaTipo1Service(IitensBaixasTipo1Repository repository)
        {
            _repository = repository;
        }
        public async Task AtualizarMatricula(DateTime dataBaixa, decimal numeroAcordo, decimal matricula)
        {
            await _repository.AtualizarMatricula(dataBaixa,
                                                 numeroAcordo,
                                                 matricula);
        }

        public async Task InserirBaixa(DateTime dataBaixa, decimal matricula, decimal numeroAcordo, decimal multa, decimal juros, DateTime dataVencimento, decimal valorParcela, decimal? codErro)
        {
            await _repository.InserirBaixa(dataBaixa,
                                           matricula,
                                           numeroAcordo,
                                           multa,
                                           juros,
                                           dataVencimento,
                                           valorParcela,
                                           codErro);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
