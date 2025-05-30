using GetAwaitService.Services.UserActivityService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.UserReaction;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.UserActivityService
{
    public class UserReactionApiService : IUserReactionApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserReactionApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserActivityServiceClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<UserReactionDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/UserReaction/GetAllReactions");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<UserReactionDTO>>(body, _jsonOptions);
        }

        public async Task<UserReactionDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/UserReaction/GetReactionById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserReactionDTO>(body, _jsonOptions);
        }

        public async Task<UserReactionDTO?> CreateAsync(UserReactionDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/UserReaction/CreateReaction", content);
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserReactionDTO>(body, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(Guid id, UserReactionDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/UserReaction/UpdateReaction/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/UserReaction/DeleteReaction/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
