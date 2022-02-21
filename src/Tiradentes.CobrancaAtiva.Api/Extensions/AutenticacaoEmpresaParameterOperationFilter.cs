using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tiradentes.CobrancaAtiva.Api.Extensions
{
    public class AutenticacaoEmpresaParameterOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var rotaDaEmpresa = context.ApiDescription.ActionDescriptor.EndpointMetadata.Any(em =>
                em.GetType() == typeof(AutenticacaoEmpresaAttribute));

            if (!rotaDaEmpresa) return;
            
            operation.Parameters ??= new List<OpenApiParameter>();
                
            operation.Parameters.Add(new OpenApiParameter 
            {
                Name = "CHAVE-EMPRESA",
                In = ParameterLocation.Header,
                Description = "Chave da Empresa",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Description = "Chave da empresa sem caracteres especiais"
                }
            });
            
            operation.Parameters.Add(new OpenApiParameter 
            {
                Name = "SENHA-EMPRESA",
                In = ParameterLocation.Header,
                Description = "Senha da Empresa",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Description = "Senha da empresa"
                }
            });
        }
    }
}