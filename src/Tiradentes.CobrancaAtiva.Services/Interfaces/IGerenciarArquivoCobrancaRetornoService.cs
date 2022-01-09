using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IGerenciarArquivoCobrancaRetornoService
    {
        void Gerenciar(IEnumerable<dynamic> arquivos);
    }
}
