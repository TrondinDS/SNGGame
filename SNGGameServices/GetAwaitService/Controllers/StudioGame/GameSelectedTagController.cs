using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedTag;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameSelectedTagController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameSelectedTagController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGameSelectedTags()
        {
            var response = await _httpClient.GetAsync("api/GameSelectedTag/GetAllGameSelectedTags");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var gameSelectedTags = JsonSerializer.Deserialize<IEnumerable<GameSelectedTagDTO>>(responseBody, _jsonOptions);
                return Ok(gameSelectedTags);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка связей 'Игра-Тег'.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameSelectedTagById(int id)
        {
            var response = await _httpClient.GetAsync($"api/GameSelectedTag/GetGameSelectedTagById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var gameSelectedTag = JsonSerializer.Deserialize<GameSelectedTagDTO>(responseBody, _jsonOptions);
                return Ok(gameSelectedTag);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении связи 'Игра-Тег' по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameSelectedTag([FromBody] GameSelectedTagDTO gameSelectedTagDto)
        {
            var jsonContent = JsonSerializer.Serialize(gameSelectedTagDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/GameSelectedTag/CreateGameSelectedTag", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdGameSelectedTag = JsonSerializer.Deserialize<GameSelectedTagDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetGameSelectedTagById), new { id = createdGameSelectedTag.Id }, createdGameSelectedTag);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании связи 'Игра-Тег'.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameSelectedTag(int id, [FromBody] GameSelectedTagDTO gameSelectedTagDto)
        {
            if (id != gameSelectedTagDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(gameSelectedTagDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/GameSelectedTag/UpdateGameSelectedTag/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении связи 'Игра-Тег'.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameSelectedTag(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/GameSelectedTag/DeleteGameSelectedTag/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении связи 'Игра-Тег'.");
        }
    }
}