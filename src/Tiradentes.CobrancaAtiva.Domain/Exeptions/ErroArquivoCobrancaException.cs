using System;
using Tiradentes.CobrancaAtiva.Domain.Enum;

namespace Tiradentes.CobrancaAtiva.Domain.Exeptions
{
    #pragma warning disable S3925 // "ISerializable" should be implemented correctly
    public class ErroArquivoCobrancaException : Exception
    #pragma warning restore S3925 // "ISerializable" should be implemented correctly
    {
        public ErrosBaixaPagamento Erro { get; set; }
        public ErroArquivoCobrancaException(ErrosBaixaPagamento erro)
        {
            Erro = erro;
        }
    }
}
