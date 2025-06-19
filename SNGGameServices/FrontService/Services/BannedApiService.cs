using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using System.Text.Json;
using System.Text;
using FrontService.Services.Interfaces;

namespace FrontService.Services
{
    /// <summary>
    /// Сервис для взаимодействия с API контроллером BannedController.
    /// </summary>
    public class BannedApiService : IBannedApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public BannedApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Получить все записи банов.
        /// </summary>
        public async Task<IEnumerable<BannedDTO>?> GetAllBannedAsync()
        {
            var response = await _httpClient.GetAsync("api/Banned/GetAllBanned");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<BannedDTO>>(body, _jsonOptions);
        }

        /// <summary>
        /// Получить список банов по ID пользователя.
        /// </summary>
        public async Task<IEnumerable<BannedDTO>?> GetBannedsByUserIdAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"api/Banned/GetBannedsByUserId/{userId}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<BannedDTO>>(body, _jsonOptions);
        }

        /// <summary>
        /// Получить конкретный бан по его ID.
        /// </summary>
        public async Task<BannedDTO?> GetBannedByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Banned/GetBannedById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BannedDTO>(body, _jsonOptions);
        }

        /// <summary>
        /// Создать новую запись бана.
        /// </summary>
        public async Task<BannedDTO?> CreateBannedAsync(BannedCreateDTO createDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(createDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Banned/CreateBanned", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при создании бана: {response.StatusCode}. Детали: {error}");
                return null;
            }

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BannedDTO>(body, _jsonOptions);
        }

        /// <summary>
        /// Обновить существующую запись бана.
        /// </summary>
        public async Task<bool> UpdateBannedAsync(BannedDTO bannedDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(bannedDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Banned/UpdateBanned/{bannedDto.Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при обновлении бана: {response.StatusCode}. Детали: {error}");
            }

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Удалить запись бана по ID.
        /// </summary>
        public async Task<bool> DeleteBannedAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Banned/DeleteBanned/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при удалении бана: {response.StatusCode}. Детали: {error}");
            }

            return response.IsSuccessStatusCode;
        }
    }
}
