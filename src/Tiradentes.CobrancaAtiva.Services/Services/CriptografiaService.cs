using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tiradentes.CobrancaAtiva.Application.Configuration;
using Tiradentes.CobrancaAtiva.Services.Interfaces;

namespace Tiradentes.CobrancaAtiva.Services.Services
{
    public class CriptografiaService : ICriptografiaService
    {
        private readonly HttpClient _httpClient;
        private readonly EncryptationConfig _config;

        public CriptografiaService(EncryptationConfig config, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClient = client;
        }

        public async Task<string> Criptografar(string dado)
        {
            return await ChamadaApi(_config.EncryptAuthorization, dado, "encrypt-value");
        }

        public async Task<string> Descriptografar(string dado)
        {
            return await ChamadaApi(_config.DecryptAuthorization, dado, "decrypt-value");
        }

        private async Task<string> ChamadaApi(string auth, string dado, string rota)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);

            var data = JsonSerializer.Serialize(dado);
            var response = await _httpClient.PostAsync(rota,
                new StringContent(data, Encoding.UTF8));

            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<string>(responseData);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
            }
        }
    }
}