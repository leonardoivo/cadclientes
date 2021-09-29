namespace Tiradentes.CobrancaAtiva.Domain.Models
{
    public class InstituicaoModalidadeModel
    {
        public int InstituicaoId { get; private set; }
        public int ModalidadeId { get; private set; }

        public virtual InstituicaoModel Instituicao { get; private set; }
        public virtual ModalidadeModel Modalidade { get; private set; }
    }
}
