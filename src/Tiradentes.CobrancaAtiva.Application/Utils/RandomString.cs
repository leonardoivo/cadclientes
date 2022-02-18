using System;

namespace Tiradentes.CobrancaAtiva.Application.Utils
{
    public static class RandomString
    {
        public static string GeneratePassword(int tamanho, int naoAlfanuméricos)
        {
            var caracteres = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            var caracteresEspeciais = "!@#$%^&*()_-+=[{]};:<>|./?";
            var rd = new Random();

            var dado = new char[tamanho];
            var pos = new int[tamanho];
            var index = 0;
            //Gera valores aleatorio e insere no array pos
            while (index < tamanho - 1)
            {
                var flag = false;
                var temp = rd.Next(0, tamanho);
                for (var j = 0; j < tamanho; j++)
                    if (temp == pos[j])
                    {
                        flag = true;
                        j = tamanho;
                    }

                if (flag) continue;
                
                pos[index] = temp;
                index++;
            }

            //Gera valores aleatorios alfanuméricos
            for (index = 0; index < tamanho - naoAlfanuméricos; index++)
                dado[index] = caracteres[rd.Next(0, caracteres.Length)];

            //Gera valores aleatorios não alfanuméricos
            for (index = tamanho - naoAlfanuméricos; index < tamanho; index++)
                dado[index] = caracteresEspeciais[rd.Next(0, caracteresEspeciais.Length)];

            //Seta os valores do array ordenada pelo array pos para a posição correta
            var stringAleatoria = new char[tamanho];
            for (index = 0; index < tamanho; index++)
                stringAleatoria[index] = dado[pos[index]];

            return new string(stringAleatoria);
        }
    }
}