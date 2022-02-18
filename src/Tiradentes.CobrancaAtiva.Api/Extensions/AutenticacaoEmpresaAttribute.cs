using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Extensions
{
    public class AutenticacaoEmpresaAttribute : TypeFilterAttribute
    {
        public AutenticacaoEmpresaAttribute() : base(typeof(AutenticacaoEmpresaFilter))
        {
            Arguments = System.Array.Empty<object>();
        }
    }

    public class AutenticacaoEmpresaFilter : IAsyncAuthorizationFilter
    {
        private readonly IEmpresaParceiraService _service;

        public AutenticacaoEmpresaFilter(IEmpresaParceiraService service)
        {
            _service = service;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var validaErro = new ValidaErro();
            if (!context.HttpContext.Request.Headers.TryGetValue("CHAVE-EMPRESA", out var cnpj))
            {
                validaErro.ExisteErro = true;
                validaErro.Erros.Add("Chave da empresa não informada");
            }

            if (!context.HttpContext.Request.Headers.TryGetValue("SENHA-EMPRESA", out var senha))
            {
                validaErro.ExisteErro = true;
                validaErro.Erros.Add("Senha da empresa não informada");
            }

            if (validaErro.ExisteErro)
            {
                context.Result = TratarResult(HttpStatusCode.Unauthorized, new {erros = validaErro.Erros});
                return;
            }

            var empresaParceira = await _service.BuscarPorCnpj(cnpj);

            if (empresaParceira is null || empresaParceira.SenhaApi != senha)
            {
                context.Result = TratarResult(HttpStatusCode.BadRequest, new {erro = "Chave ou Senha da empresa inválida"});
                return;
            }
            
            var claimsIdentity = context.HttpContext.User.Identity;
            var identity = new ClaimsIdentity(claimsIdentity);
            identity.AddClaim(new Claim("CNPJ", empresaParceira.CNPJ));
            context.HttpContext.User.AddIdentity(identity);
        }

        private static ContentResult TratarResult(HttpStatusCode statusCode, object mensagem)
        {
            return TratarStringResult(statusCode, JsonSerializer.Serialize(mensagem));
        }

        private static ContentResult TratarStringResult(HttpStatusCode statusCode, string mensagem)
        {
            return new ContentResult
            {
                StatusCode = (int) statusCode,
                Content = mensagem,
                ContentType = "application/json"
            };
        }
    }

    public class ValidaErro
    {
        public bool ExisteErro { get; set; }
        public List<string> Erros { get; set; }

        public ValidaErro()
        {
            ExisteErro = false;
            Erros = new List<string>();
        }
    }
}