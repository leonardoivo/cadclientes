namespace Tiradentes.CobrancaAtiva.Application.ViewModels.EmpresaParceira
{
    class ContaBancariaEmpresaParceiraViewModel
    {
        public int Id { get; set; }
        public string ContaCorrente { get; set; }
        public string CodigoAgencia { get; set; }
        public string Pix { get; set; }
        public string Convenio { get; set; }
        public int BancoId { get; set; }
    }
}
