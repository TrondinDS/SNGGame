using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using System.Text;
using System.Text.Json;
using FrontService.Services.Interfaces;

namespace FrontService.Services
{
    /// <summary>
    /// Сервис для взаимодействия с API контроллером EventController.
    /// </summary>
    public class EventApiService : IEventApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EventApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        /// <summary>
        /// Получение всех событий.
        /// </summary>
        public async Task<IEnumerable<EventDTO>?> GetAllEventsAsync()
        {
            var response = await _httpClient.GetAsync("api/Event/GetAll");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<EventDTO>>(body, _jsonOptions);
        }

        /// <summary>
        /// Получение события по ID.
        /// </summary>
        public async Task<EventDTO?> GetEventByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Event/GetById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EventDTO>(body, _jsonOptions);
        }

        /// <summary>
        /// Создание нового события.
        /// </summary>
        public async Task<EventDTO?> CreateEventAsync(EventDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Event/Create", content);

            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EventDTO>(body, _jsonOptions);
        }

        /// <summary>
        /// Обновление существующего события.
        /// </summary>
        public async Task<bool> UpdateEventAsync(EventDTO dto)
        {
            var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("api/Event/Update", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при обновлении события: {response.StatusCode}. Детали: {error}");
            }

            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Удаление события по ID.
        /// </summary>
        public async Task<bool> DeleteEventAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Event/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Получение отфильтрованного списка событий.
        /// </summary>
        public async Task<IEnumerable<EventDTO>?> FilterEventsAsync(ParamQueryEvent query)
        {
            var content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Event/Filter", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Ошибка при фильтрации событий: {response.StatusCode}. Детали: {error}");
                return null;
            }

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<EventDTO>>(body, _jsonOptions);
        }
    }
}
