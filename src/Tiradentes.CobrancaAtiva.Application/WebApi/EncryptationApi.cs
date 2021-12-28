using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Configuration;

namespace Tiradentes.CobrancaAtiva.Application.WebApi
{
    public class EncryptationApi
    {
        private readonly HttpClient client = new HttpClient();
        private readonly EncryptationConfig _config;

        public EncryptationApi(EncryptationConfig config)
        {
            client.BaseAddress = new Uri(config.BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _config = config;
        }
        
        public async Task<string> CallEncrypt(string value)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _config.EncryptAuthorization);

            value = value.Replace("\"", "");

            HttpResponseMessage response = await client.PostAsync("encrypt-value", new StringContent("\"" + value + "\"", Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseData = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            return responseData;
        }

        public async Task<string> CallDecrypt(string value)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _config.DecryptAuthorization);

            value = value.Replace("\"", "");

            HttpResponseMessage response = await client.PostAsync("decrypt-value", new StringContent("\"" + value + "\"", Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseData = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            return responseData;
        }
    }
}