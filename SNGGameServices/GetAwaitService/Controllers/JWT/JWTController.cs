using GetAwaitService.Auth.JWT.Service;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.JWT
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        private readonly IAuthServiceJWT _authService;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public JWTController(IAuthServiceJWT authService, IHttpClientFactory httpClientFactory)
        {
            _authService = authService;
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр [[4]]
            };
        }


        [HttpGet("{id}")]
        private async Task<IActionResult> GetUserById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/User/GetUserById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<UserDTO>(responseBody, _jsonOptions);
                return Ok(user);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении пользователя");
        }

        [HttpPost]
        private async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDto)
        {
            var jsonContent = JsonSerializer.Serialize(userDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/User/CreateUser", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdUser = JsonSerializer.Deserialize<UserDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании пользователя");
        }

        [HttpGet]
        public async Task<IActionResult> LoginServiceJWT(ulong userTelegramId)
        {
            var result = await _authService.Login(userTelegramId);
            return Ok(result);
        }
    }

}
