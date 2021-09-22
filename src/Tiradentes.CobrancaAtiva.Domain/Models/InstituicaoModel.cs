﻿using System.Collections.Generic;

namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class InstituicaoModel : BaseModel
    {
        public string Instituicao { get; set; }

        public virtual ICollection<InstituicaoModalidadeModel> InstituicoesModalidades { get; set; }
        public ICollection<HonorarioEmpresaParceiraModel> Honorarios { get; private set; }
    }
}
