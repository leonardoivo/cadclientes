using System;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Infrastructure.Context;
using GestaoClientes.Infrastructure.Repositories;
using GestaoClientes.Services.Interfaces;
using GestaoClientes.Services.Services;

namespace GestaoClientes.CrossCutting.IoC
{
    public static class NativeCoreDependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            #region Repositorios

            
            services.AddScoped<IClienteRepository, ClienteRepository>();


            #endregion

            #region Services

            services.AddScoped<IClienteService, ClienteService>();

           
            #endregion


            services.AddDbContext<GestaoClientesDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Empresas")));


            return services;
        }
    }
}