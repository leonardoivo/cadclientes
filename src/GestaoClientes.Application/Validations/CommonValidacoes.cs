using System.Collections.Generic;
using System.Linq;

namespace GestaoClientes.Application.Validations
{
    public class CommonValidacoes
    {
        public static bool ValidarCnpj(string cnpj)
        {
            if (cnpj == null) return false;

            var cnpjNumeros = Utils.ApenasNumeros(cnpj);

            if (cnpjNumeros.Length != 14) return false;

            string[] cnpjsInvalidos =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };

            if (cnpjsInvalidos.Contains(cnpjNumeros)) return false;

            var numero = cnpjNumeros.Substring(0, 14 - 2);

            var digitoVerificador = new DigitoVerificador(numero)
                .ComMultiplicadoresDeAte(2, 9)
                .Substituindo("0", 10, 11);
            var primeiroDigito = digitoVerificador.CalculaDigito();
            digitoVerificador.AddDigito(primeiroDigito);
            var segundoDigito = digitoVerificador.CalculaDigito();

            return string.Concat(primeiroDigito, segundoDigito) == cnpjNumeros.Substring(14 - 2, 2);
        }
    }

    public class Utils
    {
        public static string ApenasNumeros(string valor)
        {
            var onlyNumber = "";
            foreach (var s in valor)
            {
                if (char.IsDigit(s))
                {
                    onlyNumber += s;
                }
            }
            return onlyNumber.Trim();
        }
    }

    public class DigitoVerificador
    {
        private string _numero;
        private const int Modulo = 11;
        private readonly List<int> _multiplicadores = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9 };
        private readonly IDictionary<int, string> _substituicoes = new Dictionary<int, string>();
        private readonly bool _complementarDoModulo = true;

        public DigitoVerificador(string numero)
        {
            _numero = numero;
        }

        public DigitoVerificador ComMultiplicadoresDeAte(int primeiroMultiplicador, int ultimoMultiplicador)
        {
            _multiplicadores.Clear();
            for (var i = primeiroMultiplicador; i <= ultimoMultiplicador; i++)
                _multiplicadores.Add(i);

            return this;
        }

        public DigitoVerificador Substituindo(string substituto, params int[] digitos)
        {
            foreach (var i in digitos)
            {
                _substituicoes[i] = substituto;
            }
            return this;
        }

        public void AddDigito(string digito)
        {
            _numero = string.Concat(_numero, digito);
        }

        public string CalculaDigito()
        {
            return (_numero.Length <= 0) ? "" : GetDigitSum();
        }

        private string GetDigitSum()
        {
            var soma = 0;
            for (int i = _numero.Length - 1, m = 0; i >= 0; i--)
            {
                var produto = (int)char.GetNumericValue(_numero[i]) * _multiplicadores[m];
                soma += produto;

                if (++m >= _multiplicadores.Count) m = 0;
            }

            var mod = (soma % Modulo);
            var resultado = _complementarDoModulo ? Modulo - mod : mod;

            return _substituicoes.ContainsKey(resultado) ? _substituicoes[resultado] : resultado.ToString();
        }
    }
}
