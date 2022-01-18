using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Cobranca;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Controllers
{
    [ApiController]
    [Route("baixa-cobranca")]
    public class CobrancaController : ControllerBase
    {
        private readonly ICobrancaService _cobrancaService;
        private readonly IBaixasCobrancasService _baixasCobrancaService;
        public CobrancaController(ICobrancaService cobrancaService,
                                  IBaixasCobrancasService baixasCobrancaService)
        {
            _cobrancaService = cobrancaService;
            _baixasCobrancaService = baixasCobrancaService;
        }

        /// <summary>
        /// Cria um registro de Resposta cobrnaça.
        /// </summary>
        /// <param name="resposta">Objeto recebido como Json</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST               
        ///     {
        ///        "tipoRegistro": "string", Obrigatorio 
        ///        "cpf": "string", Obrigatorio
        ///        "numeroAcordo": "string", Obrigatorio
        ///        "parcela": "string", Obrigatorio
        ///        "cnpjEmpresaCobranca": "string", Obrigatorio
        ///        "situacaoAluno": "string",
        ///        "sistema": "string", Obrigatorio
        ///        "tipoInadimplencia": "string", Obrigatorio
        ///        "chaveInadimplencia": "string",
        ///        "matricula": "string", Obrigatorio
        ///        "periodo": "202103"(2021 - ANO, 03 - Semestre),  Obrigatorio
        ///        "idTitulo": "string", Necessario em Titulos Avulsos
        ///        "codigoAtividade": "string",
        ///        "numeroEvt": "string",
        ///        "idPessoa": "string", Obrigatorio
        ///        "codigoBanco": "string",
        ///        "codigoAgencia": "string",
        ///        "numeroConta": "string",
        ///        "numeroCheque": "string",
        ///        "juros": "string",
        ///        "multa": "string",
        ///        "valorTotal": "string",
        ///        "dataFechamentoAcordo": "string",
        ///        "totalParcelas": "string",
        ///        "dataVencimento": "string",
        ///        "valorParcela": "string",
        ///        "saldoDevedorTotal": "string",
        ///        "produto": "string",
        ///        "descricaoProduto": "string",
        ///        "fase": "string",
        ///        "codigoControleCliente": "string",
        ///        "nossoNumero": "string",
        ///        "dataPagamento": "string",
        ///        "dataBaixa": "string",
        ///        "valorPago": "string",
        ///        "tipoPagamento": "string"
        ///     }
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] RespostaViewModel resposta)
        {
            return Ok(await _cobrancaService.Criar(resposta));
        }

        /// <summary>
        /// Retorna o Historico do processamnento de arquivos.
        /// </summary>
        /// <param name="dataBaixa">dd-mm-yyyy</param>
        /// <returns></returns>
        [HttpGet("resultado/{dataBaixa}")]
        public async Task<IActionResult> BuscarHistoricoProcessamentoCobranca(string dataBaixa)
        {
            return Ok(await _baixasCobrancaService.Buscar(Convert.ToDateTime(dataBaixa)));
        }
    }
}