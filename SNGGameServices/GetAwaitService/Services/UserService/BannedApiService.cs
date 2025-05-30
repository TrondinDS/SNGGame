using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.UserService
{
    public class BannedApiService : IBannedApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public BannedApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<BannedDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Banned/GetAllBanned");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<BannedDTO>>(content, _jsonOptions);
        }

        public async Task<BannedDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Banned/GetBannedById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BannedDTO>(content, _jsonOptions);
        }

        public async Task<BannedDTO?> CreateAsync(BannedDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Banned/CreateBanned", content);
            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BannedDTO>(responseBody, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(Guid id, BannedDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Banned/UpdateBanned/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Banned/DeleteBanned/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
