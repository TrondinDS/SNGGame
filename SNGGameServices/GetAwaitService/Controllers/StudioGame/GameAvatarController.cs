using Library.Generics.DB.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameAvatarController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameAvatarController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ImgDTO imgDto)
        {
            var jsonContent = JsonSerializer.Serialize(imgDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/GameAvatar/Create", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Аватар игры успешно загружен.");
            }

            return StatusCode((int)response.StatusCode, "Ошибка при загрузке аватара игры.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var response = await _httpClient.GetAsync($"api/GameAvatar/GetByUserId/{id}");

            if (response.IsSuccessStatusCode)
            {
                var contentType = response.Content.Headers.ContentType?.ToString();
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                return File(fileBytes, contentType ?? "application/octet-stream");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении аватара игры.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ImgDTO imgDto)
        {
            var jsonContent = JsonSerializer.Serialize(imgDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/GameAvatar/Update", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Аватар игры успешно обновлен.");
            }

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении аватара игры.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/GameAvatar/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Ok("Аватар игры успешно удален.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении аватара игры.");
        }
    }
}