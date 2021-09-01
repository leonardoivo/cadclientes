using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Tiradentes.CobrancaAtiva.Repositories.Aluno;
using Tiradentes.CobrancaAtiva.Repositories.Interface.Aluno;

namespace Tiradentes.CobrancaAtiva.Infra.Injections.Repositories
{
    public class RepositoriesInjection
    {
        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IAlunoRepository, AlunoRepository>();
        }
    }
}
