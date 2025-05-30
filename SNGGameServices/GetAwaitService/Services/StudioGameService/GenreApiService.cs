using GetAwaitService.Services.StudioGameService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.StudioGameService
{
    public class GenreApiService : IGenreService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GenreApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<GenreDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Genre/GetAllGenres");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GenreDTO>>(json, _jsonOptions);
        }

        public async Task<GenreDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Genre/GetGenreById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GenreDTO>(json, _jsonOptions);
        }

        public async Task<GenreDTO?> CreateAsync(GenreDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Genre/CreateGenre", content);
            if (!response.IsSuccessStatusCode) return null;

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GenreDTO>(jsonResponse, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(GenreDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Genre/UpdateGenre/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Genre/DeleteGenre/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
