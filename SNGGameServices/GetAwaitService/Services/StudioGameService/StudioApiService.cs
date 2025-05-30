using GetAwaitService.Services.StudioGameService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.StudioGameService
{
    public class StudioApiService : IStudioService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public StudioApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<StudioDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Studio/GetAllStudios");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<StudioDTO>>(json, _jsonOptions);
        }

        public async Task<StudioDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Studio/GetStudioById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<StudioDTO>(json, _jsonOptions);
        }

        public async Task<StudioDTO?> CreateAsync(StudioDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Studio/CreateStudio", content);
            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<StudioDTO>(responseBody, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(StudioDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Studio/UpdateStudio/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Studio/DeleteStudio/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
