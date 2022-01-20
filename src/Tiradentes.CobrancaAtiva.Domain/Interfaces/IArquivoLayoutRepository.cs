﻿using System;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Domain.Interfaces
{
    public interface IArquivoLayoutRepository : IBaseRepository<ArquivoLayoutModel>
    {
        ArquivoLayoutModel BuscarPorDataHora(DateTime dataHora);
        ArquivoLayoutModel BuscarLayoutSucessoPorData(DateTime dataHora);
        void HabilitarAlteracaoArquivoLayout(bool status);
    }
}
