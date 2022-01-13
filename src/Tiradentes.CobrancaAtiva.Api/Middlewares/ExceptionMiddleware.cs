using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Utils;
using Tiradentes.CobrancaAtiva.Domain.Collections;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private IApplicationErrorService _service;

        public ExceptionMiddleware(
            RequestDelegate next, 
            ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }


        public async Task InvokeAsync(HttpContext httpContext, IApplicationErrorService service)
        {
            _service = service;
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                await TratarErro(((int)ex.StatusCode), ex.Message, ex.StackTrace, httpContext);
            }
            catch (Exception ex)
            {
                if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Production")
                {
                    _logger.LogError(ex.Message);
                    await TratarErro(500, 
                                    JsonSerializer.Serialize(new { erro = ex.Message, innerException = ex.InnerException?.Message }),
                                    ex.StackTrace,
                                    httpContext);
                }
                else 
                {
                    await TratarErro(500, JsonSerializer.Serialize(new { erro = "Erro inesperado" }), ex.StackTrace, httpContext);
                }
            }
        }

        private async Task TratarErro(int statusCode, string message, string stacktrace, HttpContext context)
        {
            var bytes = Encoding.UTF8.GetBytes(message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);

            await _service.LogError(new ApplicationErrorCollection() 
            {
                Sistema = "MEC",
                DataHora = DateTime.Now,
                Mensagem = message,
                Stacktrace = stacktrace
            });
        }
    }
}
