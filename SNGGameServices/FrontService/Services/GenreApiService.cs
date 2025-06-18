using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;
using System.Text.Json;
using System.Text;

namespace FrontService.Services
{
    public class GenreApiService : IGenreApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GenreApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<GenreDTO>?> GetAllGenresAsync()
        {
            var response = await _httpClient.GetAsync("api/Genre/GetAllGenres");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GenreDTO>>(content, _jsonOptions);
        }

        public async Task<GenreDTO?> GetGenreByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Genre/GetGenreById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GenreDTO>(content, _jsonOptions);
        }

        public async Task<GenreDTO?> CreateGenreAsync(GenreCreateDTO genreDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(genreDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Genre/CreateGenre", content);

            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GenreDTO>(body, _jsonOptions);
        }

        public async Task<bool> UpdateGenreAsync(Guid id, GenreDTO genreDto)
        {
            if (id != genreDto.Id) return false;

            var content = new StringContent(JsonSerializer.Serialize(genreDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Genre/UpdateGenre/{id}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGenreAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Genre/DeleteGenre/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
