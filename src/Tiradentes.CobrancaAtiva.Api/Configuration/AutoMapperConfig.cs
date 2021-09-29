﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tiradentes.CobrancaAtiva.Application.AutoMapper;

namespace Tiradentes.CobrancaAtiva.Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AutoMapperServiceConfig(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var mappginConfig = AutoMapperSetup.RegisterMappings();
            IMapper mapper = mappginConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
