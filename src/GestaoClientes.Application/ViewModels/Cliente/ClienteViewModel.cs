using GestaoClientes.Domain.Enums;

namespace GestaoClientes.Application.ViewModels.Cliente

{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Porte Porte { get; set; }
    }
}