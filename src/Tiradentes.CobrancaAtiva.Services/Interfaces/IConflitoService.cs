﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.QueryParams;
using Tiradentes.CobrancaAtiva.Application.ViewModels;
using Tiradentes.CobrancaAtiva.Application.ViewModels.Conflito;
using Tiradentes.CobrancaAtiva.Domain.QueryParams;

namespace Tiradentes.CobrancaAtiva.Services.Interfaces
{
    public interface IConflitoService : IDisposable
    {
        Task<ViewModelPaginada<BuscaConflitoViewModel>> Buscar(ConsultaConflitoQueryParam queryParam);
    }
}