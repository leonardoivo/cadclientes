using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Domain.Models;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class BaixasCobrancasService : IBaixasCobrancasService
    {
        readonly IBaixasCobrancasRepository _baixasCobrancasRepository;
        public BaixasCobrancasService(IBaixasCobrancasRepository baixasCobrancasRepository)
        {
            _baixasCobrancasRepository = baixasCobrancasRepository;
        }
        public async Task AtualizarBaixasCobrancas(BaixasCobrancasViewModel baixasCobrancas)
        {
            //Update do doc n faz sentido
            var baixaCobranca = await _baixasCobrancasRepository.BuscarPorDataBaixa(baixasCobrancas.DataBaixa);



            await _baixasCobrancasRepository.Alterar(baixaCobranca);
        }

        public async Task CriarBaixasCobrancas(DateTime dataBaixa)
        {
            await _baixasCobrancasRepository.Criar(new BaixasCobrancasModel()
            {
                DataBaixa = dataBaixa
                
            });
        }
    }
}
