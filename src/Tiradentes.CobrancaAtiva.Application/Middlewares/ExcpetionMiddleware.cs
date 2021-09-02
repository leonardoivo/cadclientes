using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Utils;

namespace Tiradentes.CobrancaAtiva.Application.Middlewares
{
    public class ExcpetionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExcpetionMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                await TratarErro(((int)ex.StatusCode), ex.Message, httpContext);
            }
            catch (Exception ex)
            {
                await TratarErro(500, JsonSerializer.Serialize(new { erro = "Erro inesperado" }), httpContext);
            }
        }

        private async Task TratarErro(int statusCode, string message, HttpContext context)
        {
            var bytes = Encoding.UTF8.GetBytes(message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
