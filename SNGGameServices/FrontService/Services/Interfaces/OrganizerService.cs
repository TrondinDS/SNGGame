using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using System.Text.Json;
using System.Text;

namespace FrontService.Services.Interfaces;

public class OrganizerService : IOrganizerService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public OrganizerService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
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

    public async Task<IEnumerable<OrganizerDTO>?> GetByUserId(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/Organizer/GetByUserId/{id}");
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<IEnumerable<OrganizerDTO>?>(_jsonOptions);
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
        var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync("api/Organizer/Update", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Ошибка при обновлении игры: {response.StatusCode}. Детали: {error}");
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> Delete(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"api/Organizer/Delete/{id}");
        return response.IsSuccessStatusCode;
    }
}
