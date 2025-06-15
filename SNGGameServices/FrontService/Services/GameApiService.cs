using System.Text;
using System.Text.Json;
using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame;
using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;

namespace FrontRazor.Services.GameService
{
    /// <summary>
    /// Сервис для взаимодействия с API контроллером GameController.
    /// </summary>
    public class GameApiService : IGameApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<GameDTO>?> GetAllGamesAsync()
        {
            var response = await _httpClient.GetAsync("api/Game/GetAllGames");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GameDTO>>(body, _jsonOptions);
        }

        public async Task<GameDTO?> GetGameByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Game/GetGameById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameDTO>(body, _jsonOptions);
        }

        public async Task<IEnumerable<GameDTO>?> GetFilteredGamesAsync(ParamQueryGame query)
        {
            var content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Game/GetFilterGame", content);

            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GameDTO>>(body, _jsonOptions);
        }

        public async Task<GameDTO?> CreateGameAsync(GameCreateDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Game/CreateGame", content);

            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GameDTO>(body, _jsonOptions);
        }

        public async Task<bool> UpdateGameAsync(GameDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/Game/UpdateGame", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при обновлении игры: {response.StatusCode}. Детали: {error}");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGameAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Game/DeleteGame/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<GameDTOView>?> GetGameDTOViewByIdGamesAsync(List<Guid> gameIds)
        {
            var content = new StringContent(JsonSerializer.Serialize(gameIds), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Game/GetGameDTOViewByIdGames", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при получении GameDTOView: {response.StatusCode}. Детали: {error}");
                return null;
            }

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<GameDTOView>>(body, _jsonOptions);
        }

    }
}
