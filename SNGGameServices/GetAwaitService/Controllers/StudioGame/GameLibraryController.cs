using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameLibraryController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameLibraryController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGameLibraries()
        {
            var response = await _httpClient.GetAsync("api/GameLibrary/GetAllGameLibraries");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var gameLibraries = JsonSerializer.Deserialize<IEnumerable<GameLibraryDTO>>(responseBody, _jsonOptions);
                return Ok(gameLibraries);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка библиотек игр.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameLibraryById(int id)
        {
            var response = await _httpClient.GetAsync($"api/GameLibrary/GetGameLibraryById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var gameLibrary = JsonSerializer.Deserialize<GameLibraryDTO>(responseBody, _jsonOptions);
                return Ok(gameLibrary);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении записи библиотеки игр по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameLibrary([FromBody] GameLibraryDTO gameLibraryDto)
        {
            var jsonContent = JsonSerializer.Serialize(gameLibraryDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/GameLibrary/CreateGameLibrary", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdGameLibrary = JsonSerializer.Deserialize<GameLibraryDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetGameLibraryById), new { id = createdGameLibrary.Id }, createdGameLibrary);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании записи в библиотеке игр.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameLibrary(int id, [FromBody] GameLibraryDTO gameLibraryDto)
        {
            if (id != gameLibraryDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(gameLibraryDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/GameLibrary/UpdateGameLibrary/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении записи в библиотеке игр.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameLibrary(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/GameLibrary/DeleteGameLibrary/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении записи из библиотеки игр.");
        }
    }
}