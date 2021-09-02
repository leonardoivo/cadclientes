using System.Text.Json;
using Tiradentes.CobrancaAtiva.Application.Utils;

namespace Tiradentes.CobrancaAtiva.Services
{
    public class BaseService
    {
        protected void EntidadeNaoEncontrada(string mensagem)
        {
            throw CustomException.EntityNotFound(JsonSerializer.Serialize(new { erro = mensagem }));
        }
    }
}
