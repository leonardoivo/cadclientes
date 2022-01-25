using System;

namespace Tiradentes.CobrancaAtiva.Domain.Collections
{
    public class ApplicationErrorCollection
    {
        public DateTime DataHora { get; set; }
        public String Sistema { get; set; }
        public String Stacktrace { get; set; }
        public String Mensagem { get; set; }
    }
}