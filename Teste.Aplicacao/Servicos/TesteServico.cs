using System;
using System.Collections.Generic;
using System.Text;
using Teste.Aplicacao.Servicos.Interfaces;
using Teste.Infraestrutura.BancoDados.Repositorios;

namespace Teste.Aplicacao.Servicos
{
    public class TesteServico : BaseService<Teste.Dominio.Entidades.Teste>, ITesteServico
    {
        
    }
}
