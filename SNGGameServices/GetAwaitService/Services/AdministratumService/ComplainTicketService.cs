using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;
using System.Text.Json;
using System.Text;
using GetAwaitService.Services.ChatFeedbackService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;
using Library.Generics.Query.QueryModels.Administratum;

namespace GetAwaitService.Services.ChatFeedbackService
{
    public class ComplainTicketService : IComplainTicketService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ComplainTicketService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AdministratumServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<ComplainTicketDTO>?> GetAll()
        {
            var response = await _httpClient.GetAsync("api/ComplainTicket/GetAll");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<IEnumerable<ComplainTicketDTO>>(_jsonOptions);
        }

        public async Task<ComplainTicketDTO?> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/ComplainTicket/GetById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<ComplainTicketDTO>(_jsonOptions);
        }

        public async Task<ComplainTicketDTO?> Create(ComplainTicketDTO dto)
        {
            var json = JsonSerializer.Serialize(dto, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/ComplainTicket/Create", content);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<ComplainTicketDTO>(_jsonOptions);
        }

        public async Task<bool> Update(ComplainTicketDTO dto)
        {
            var json = JsonSerializer.Serialize(dto, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/ComplainTicket/Update/{dto.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/ComplainTicket/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ComplainTicketDTO>?> Filter(ParamQueryComplainTicket param)
        {
            var json = JsonSerializer.Serialize(param, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/ComplainTicket/Filter", content);
            if (!response.IsSuccessStatusCode) return null;

            return await response.Content.ReadFromJsonAsync<IEnumerable<ComplainTicketDTO>>(_jsonOptions);
        }
    }
}
