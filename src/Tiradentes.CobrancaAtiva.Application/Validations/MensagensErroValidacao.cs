namespace Tiradentes.CobrancaAtiva.Application.Validations
{
    public class MensagensErroValidacao
    {
        public static string CampoObrigatorio => "Campo {PropertyName} é obrigatório.";
        public static string TamanhaMaximo => "Campo {PropertyName} deve ter no máximo {MaxLength} caracteres";
    }
}
