using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.UserService
{
    public class UserApiService : IUserApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<UserDTO>?> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("api/User/GetAllUser");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<UserDTO>>(responseBody, _jsonOptions);
        }

        public async Task<UserDTO?> GetUserByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/User/GetUserById/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDTO>(responseBody, _jsonOptions);
        }

        public async Task<UserDTO?> CreateUserAsync(UserCreateDTO userDto)
        {
            var jsonContent = JsonSerializer.Serialize(userDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/User/CreateUser", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserDTO>(responseBody, _jsonOptions);
        }

        public async Task<bool> UpdateUserAsync(Guid id, UserDTO userDto)
        {
            var jsonContent = JsonSerializer.Serialize(userDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/User/UpdateUser/{id}", httpContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/User/DeleteUser/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
