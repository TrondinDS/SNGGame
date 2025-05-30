using GetAwaitService.Services.StudioGameService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame;
using Library.Generics.Query.QueryModels.StudioGame;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.StudioGameService
{
    public class GameApiService : IGameApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<GameDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Game/GetAllGames");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GameDTO>>(body, _jsonOptions);
        }

        public async Task<GameDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Game/GetGameById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameDTO>(body, _jsonOptions);
        }

        public async Task<GameDTO?> CreateAsync(GameDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Game/CreateGame", content);
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameDTO>(body, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(Guid id, GameDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Game/UpdateGame/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Game/DeleteGame/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<GameDTO>?> GetFilteredAsync(ParamQueryGame query)
        {
            var content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Game/GetFilterGame", content);
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GameDTO>>(body, _jsonOptions);
        }

        public async Task<IEnumerable<StatisticGameDTO>?> GetStatisticsAsync(List<Guid> gameIds)
        {
            var content = new StringContent(JsonSerializer.Serialize(gameIds), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Game/GetStatisticGames", content);
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<StatisticGameDTO>>(body, _jsonOptions);
        }
    }
}
