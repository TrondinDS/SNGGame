using System.Text;
using System.Text.Json;
using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;

namespace FrontRazor.Services.UserService
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<UserDTO>?> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("api/User/GetAllUser");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<UserDTO>>(body, _jsonOptions);
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/User/GetUserById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDTO>(body, _jsonOptions);
        }

        public async Task<UserDTO?> CreateUserAsync(UserCreateDTO userDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/User/CreateUser", content);

            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDTO>(body, _jsonOptions);
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserDTO userDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/User/UpdateUser/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/User/DeleteUser/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
