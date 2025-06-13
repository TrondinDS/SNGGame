using System.Text;
using System.Text.Json;
using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;

namespace FrontRazor.Services.StudioService
{
    /// <summary>
    /// Сервис для взаимодействия с API контроллером StudioController.
    /// </summary>
    public class StudioApiService : IStudioApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public StudioApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        /// <summary>
        /// Получить все студии.
        /// </summary>
        public async Task<IEnumerable<StudioDTO>?> GetAllStudiosAsync()
        {
            var response = await _httpClient.GetAsync("api/Studio/GetAllStudios");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<StudioDTO>>(body, _jsonOptions);
        }

        /// <summary>
        /// Получить студию по её идентификатору.
        /// </summary>
        public async Task<StudioDTO?> GetStudioByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Studio/GetStudioById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<StudioDTO>(body, _jsonOptions);
        }

        /// <summary>
        /// Получить студию по идентификатору пользователя.
        /// </summary>
        public async Task<StudioDTO?> GetStudioByUserIdAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"api/Studio/GetStudioByUserId/{userId}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<StudioDTO>(body, _jsonOptions);
        }

        /// <summary>
        /// Создать новую студию.
        /// </summary>
        public async Task<StudioDTO?> CreateStudioAsync(StudioCreateDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Studio/CreateStudio", content);

            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<StudioDTO>(body, _jsonOptions);
        }

        /// <summary>
        /// Обновить данные студии.
        /// </summary>
        public async Task<bool> UpdateStudioAsync(StudioDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/Studio/UpdateStudio", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при обновлении студии: {response.StatusCode}. Детали: {error}");
            }

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Удалить студию по идентификатору.
        /// </summary>
        public async Task<bool> DeleteStudioAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Studio/DeleteStudio/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
