using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.UserSubscription;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.UserService
{
    public class UserSubscriptionApiService : IUserSubscriptionApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserSubscriptionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<UserSubscriptionDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/UserSubscription/GetAllUserSubscription");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<UserSubscriptionDTO>>(content, _jsonOptions);
        }

        public async Task<UserSubscriptionDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/UserSubscription/GetUserSubscriptionById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserSubscriptionDTO>(content, _jsonOptions);
        }

        public async Task<UserSubscriptionDTO?> CreateAsync(UserSubscriptionCreateDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/UserSubscription/CreateUserSubscription", content);
            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserSubscriptionDTO>(responseBody, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(Guid id, UserSubscriptionDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/UserSubscription/UpdateUserSubscription/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/UserSubscription/DeleteUserSubscription/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
