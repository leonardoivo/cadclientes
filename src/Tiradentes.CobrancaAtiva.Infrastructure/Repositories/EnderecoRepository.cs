using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Tiradentes.CobrancaAtiva.Domain.Interfaces;
using Tiradentes.CobrancaAtiva.Infrastructure.Context;
using Tiradentes.CobrancaAtiva.Domain.Models;

namespace Tiradentes.CobrancaAtiva.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly CobrancaAtivaDbContext _context;
        public EnderecoRepository(CobrancaAtivaDbContext context)
        {
            _context = context;
        }

        public async Task<EnderecoModel> BuscarPorCep(string CEP)
        {
            var pCep = new OracleParameter("P_CEP", OracleDbType.Varchar2, CEP, ParameterDirection.Input);
            var pUf = new OracleParameter("P_UF", OracleDbType.Varchar2, ParameterDirection.Output);
            var pCidade = new OracleParameter("P_CIDADE", OracleDbType.Varchar2, ParameterDirection.Output);
            var pBairro = new OracleParameter("P_BAIRRO", OracleDbType.Varchar2, ParameterDirection.Output);
            var pLogradouro = new OracleParameter("P_LOGRADOURO", OracleDbType.Varchar2, ParameterDirection.Output);

            pUf.Size = 100;
            pCidade.Size = 100;
            pBairro.Size = 100;
            pLogradouro.Size = 100;

            await _context.Database.ExecuteSqlRawAsync("BEGIN SCF.P_OBTER_DADOS_CEP({0}, {1}, {2}, {3}, {4}); END;", 
                pCep, pUf, pCidade, pBairro, pLogradouro );

            return new EnderecoModel(CEP, 
                                        pUf.Value.ToString(), 
                                        pCidade.Value.ToString(), 
                                        pBairro.Value.ToString(), 
                                        pLogradouro.Value.ToString());
        }
    }
}
