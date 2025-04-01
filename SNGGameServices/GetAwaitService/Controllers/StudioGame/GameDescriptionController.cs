using Library.Generics.DB.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameDescriptionController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameDescriptionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContentDTO contentDto)
        {
            var jsonContent = JsonSerializer.Serialize(contentDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/GameDescription/Create", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Описание игры успешно загружено.");
            }

            return StatusCode((int)response.StatusCode, "Ошибка при загрузке описания игры.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var response = await _httpClient.GetAsync($"api/GameDescription/GetByUserId/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody); // Возвращаем само содержимое описания
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении описания игры.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContentDTO contentDto)
        {
            var jsonContent = JsonSerializer.Serialize(contentDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("api/GameDescription/Update", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Описание игры успешно обновлено.");
            }

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении описания игры.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/GameDescription/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Ok("Описание игры успешно удалено.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении описания игры.");
        }
    }
}