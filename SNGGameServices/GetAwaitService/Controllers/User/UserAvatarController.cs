using GetAwaitService.DB.DTO.UserService.User;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAvatarController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserAvatarController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAvatarDTO userAvatarDto)
        {
            var jsonContent = JsonSerializer.Serialize(userAvatarDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/UserAvatar/Create", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Аватар успешно загружен.");
            }

            return StatusCode((int)response.StatusCode, "Ошибка при загрузке аватара.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/UserAvatar/GetByUserId/{id}");

            if (response.IsSuccessStatusCode)
            {
                var contentType = response.Content.Headers.ContentType?.ToString();
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                return File(fileBytes, contentType ?? "application/octet-stream");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении аватара.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserAvatarDTO userAvatarDto)
        {
            var jsonContent = JsonSerializer.Serialize(userAvatarDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/UserAvatar/Update", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Аватар успешно обновлен.");
            }

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении аватара.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/UserAvatar/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Ok("Аватар успешно удален.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении аватара.");
        }
    }
}