using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre;
using System.Text.Json;
using System.Text;
using GetAwaitService.Services.StudioGameService.Interfaces;

namespace GetAwaitService.Services.StudioGameService
{
    public class GameSelectedGenreApiService : IGameSelectedGenreService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameSelectedGenreApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<GameSelectedGenreDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/GameSelectedGenre/GetAllGameSelectedGenres");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GameSelectedGenreDTO>>(json, _jsonOptions);
        }

        public async Task<GameSelectedGenreDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/GameSelectedGenre/GetGameSelectedGenreById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameSelectedGenreDTO>(json, _jsonOptions);
        }

        public async Task<GameSelectedGenreDTO?> CreateAsync(GameSelectedGenreDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/GameSelectedGenre/CreateGameSelectedGenre", content);
            if (!response.IsSuccessStatusCode) return null;

            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameSelectedGenreDTO>(responseJson, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(GameSelectedGenreDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/GameSelectedGenre/UpdateGameSelectedGenre/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/GameSelectedGenre/DeleteGameSelectedGenre/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
