using System.Text.Json;
using System.Text;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using GetAwaitService.Services.OrganizerEventService.Interfaces;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;

namespace GetAwaitService.Services.OrganizerEventService
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EventService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("OrganizerEventServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<EventDTO>?> GetAll()
        {
            var response = await _httpClient.GetAsync("api/Event/GetAll");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<IEnumerable<EventDTO>>(_jsonOptions);
        }

        public async Task<IEnumerable<EventDTO>?> Filter(ParamQueryEvent param)
        {
            var json = JsonSerializer.Serialize(param, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Event/Filter", content);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<IEnumerable<EventDTO>>(_jsonOptions);
        }

        public async Task<EventDTO?> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Event/GetById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<EventDTO>(_jsonOptions);
        }

        //public async Task<IEnumerable<OrganizerDTO>?> GetByUserId(Guid id)
        //{
        //    var response = await _httpClient.GetAsync($"api/Event/GetByUserId/{id}");
        //    if (!response.IsSuccessStatusCode) return null;

        //    var json = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<IEnumerable<OrganizerDTO>>(json, _jsonOptions);
        //}

        public async Task<EventDTO?> Create(EventDTO dto)
        {
            var json = JsonSerializer.Serialize(dto, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Event/Create", content);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<EventDTO>(_jsonOptions);
        }

        public async Task<bool> Update(EventDTO dto)
        {
            var json = JsonSerializer.Serialize(dto, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Event/Update/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Event/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
