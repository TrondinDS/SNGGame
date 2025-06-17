using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using System.Text.Json;
using System.Text;
using GetAwaitService.Services.OrganizerEventService.Interfaces;

namespace GetAwaitService.Services.OrganizerEventService
{
    public class OrganizerService : IOrganizerService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public OrganizerService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("OrganizerEventServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<OrganizerDTO>?> GetAll()
        {
            var response = await _httpClient.GetAsync("api/Organizer/GetAll");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<IEnumerable<OrganizerDTO>>(_jsonOptions);
        }

        public async Task<OrganizerDTO?> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Organizer/GetById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<OrganizerDTO>(_jsonOptions);
        }

        public async Task<OrganizerDTO?> Create(OrganizerDTO dto)
        {
            var json = JsonSerializer.Serialize(dto, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Organizer/Create", content);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<OrganizerDTO>(_jsonOptions);
        }

        public async Task<bool> Update(OrganizerDTO dto)
        {
            var json = JsonSerializer.Serialize(dto, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Organizer/Update/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Organizer/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
