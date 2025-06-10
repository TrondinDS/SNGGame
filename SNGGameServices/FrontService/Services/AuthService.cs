using FrontService.Model;
using FrontService.Services.Interfaces;
using System.Text.Json;

namespace FrontService.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        /// <summary>
        /// Получает JWT токен по Telegram ID
        /// </summary>
        public async Task<string?> GetJwtTokenAsync(long telegramId)
        {


            var response = await _httpClient.GetAsync($"api/JWT/LoginServiceJWT?userTelegramId={telegramId}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }
    }
}
