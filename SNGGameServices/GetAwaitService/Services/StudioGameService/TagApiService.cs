using GetAwaitService.Services.StudioGameService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.StudioGameService
{
    public class TagApiService : ITagService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TagApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<TagDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Tag/GetAllTags");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<TagDTO>>(json, _jsonOptions);
        }

        public async Task<TagDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Tag/GetTagById/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TagDTO>(json, _jsonOptions);
        }

        public async Task<TagDTO?> CreateAsync(TagDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Tag/CreateTag", content);
            if (!response.IsSuccessStatusCode)
                return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TagDTO>(responseBody, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(TagDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Tag/UpdateTag/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Tag/DeleteTag/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
