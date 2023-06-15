using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using GestaoClientes.Application.QueryParams;
using GestaoClientes.Services.Interfaces;
using GestaoClientes.Application.ViewModels.Cliente;

namespace GestaoClientes.Api.Controllers
{
    [ApiController]
    [Route("Clientes")]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private IClienteService _clienteService;

        public ClienteController(IClienteService clienteService) {

            _clienteService = clienteService;


        }

        [HttpGet]
        public  IList<ClienteViewModel> Buscar() {

            return _clienteService.ListarClientes();

        }

        [HttpPost]
        public  ActionResult<ClienteViewModel> Criar( [FromBody] ClienteViewModel viewModel)
        {
            _clienteService.InserirCliente(viewModel);
            return NoContent();

        }

        [HttpPut]
        public  ActionResult<ClienteViewModel> Alterar(
            [FromBody] ClienteViewModel viewModel)
        {
              _clienteService.AlterarCliente(viewModel);
            return NoContent();

        }

        [HttpDelete("{clienteId:int}")]
        public  ActionResult<ClienteViewModel> Deletar(int id)
        {
            _clienteService.DeletarCliente(id);
            return NoContent();
        }
    }
}
