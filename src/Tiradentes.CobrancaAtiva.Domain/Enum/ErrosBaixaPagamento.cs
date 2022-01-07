
using System.ComponentModel;

namespace Tiradentes.CobrancaAtiva.Domain.Enum
{
    public enum ErrosBaixaPagamento
    {
        [Description("Matrícula inexistente")]
        MatriculaInexistente = 1,

        [Description("Parcela paga pelo aluno na instituição")]
        ParcelaPagaInstituicao = 2,

        [Description("Data inconsistente")]
        DataInconsistente = 3,

        [Description("Parcela títulos X Itens geração inconsistente")]
        GeracaoInconsistente = 4,

        [Description("Valor pago insuficiente")]
        ValorPagoInsuficiente = 5,

        [Description("Acordo não cadastrado")]
        AcordoNaoCadastrado = 6,
        
        [Description("Outros erros")]
        OutrosErros = 7,

        [Description("Parcela acordo já paga")]
        ParcelaAcordoJaPaga = 8,
        
        [Description("Parcela já cadastrada")]
        ParcelaJaCadastrada = 9,

        [Description("Data de vencimento da parcela menor do que a data de fechamento do acordo")]
        DataFechamentoMenorDataAcordo = 10,
        
        [Description("Pagamento não efetuado pois não existe parcelas vinculadas")]
        NaoExisteParcelasVinculadas = 11,

        [Description("Parcela já enviada anteriormente pela empresa de cobrança")]
        ParcelaEnviadaAnteriormentePelaEmpresaCobranca = 12,
        
        [Description("Arquivo com layout inconsistente")]
        LayoutInconsistente = 13

    }
}
