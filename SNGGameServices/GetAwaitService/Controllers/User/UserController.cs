using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр [[4]]
            };
        }

        // Добавляем все методы из UserService.Controllers.UserController

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var response = await _httpClient.GetAsync("api/User/GetAllUser");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<IEnumerable<UserDTO>>(responseBody, _jsonOptions);
                return Ok(users);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении пользователей");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
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
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDto)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDTO userDto)
        {
            if (id != userDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных");

            var jsonContent = JsonSerializer.Serialize(userDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/User/UpdateUser/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении пользователя");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/User/DeleteUser/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении пользователя");
        }
    }
}