using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameSelectedGenreController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameSelectedGenreController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGameSelectedGenres()
        {
            var response = await _httpClient.GetAsync("api/GameSelectedGenre/GetAllGameSelectedGenres");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var gameSelectedGenres = JsonSerializer.Deserialize<IEnumerable<GameSelectedGenreDTO>>(responseBody, _jsonOptions);
                return Ok(gameSelectedGenres);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка выбранных жанров игр.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameSelectedGenreById(int id)
        {
            var response = await _httpClient.GetAsync($"api/GameSelectedGenre/GetGameSelectedGenreById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var gameSelectedGenre = JsonSerializer.Deserialize<GameSelectedGenreDTO>(responseBody, _jsonOptions);
                return Ok(gameSelectedGenre);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении выбранного жанра игры по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameSelectedGenre([FromBody] GameSelectedGenreDTO gameSelectedGenreDto)
        {
            var jsonContent = JsonSerializer.Serialize(gameSelectedGenreDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/GameSelectedGenre/CreateGameSelectedGenre", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdGameSelectedGenre = JsonSerializer.Deserialize<GameSelectedGenreDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetGameSelectedGenreById), new { id = createdGameSelectedGenre.Id }, createdGameSelectedGenre);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании выбранного жанра игры.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameSelectedGenre(int id, [FromBody] GameSelectedGenreDTO gameSelectedGenreDto)
        {
            if (id != gameSelectedGenreDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(gameSelectedGenreDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/GameSelectedGenre/UpdateGameSelectedGenre/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении выбранного жанра игры.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameSelectedGenre(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/GameSelectedGenre/DeleteGameSelectedGenre/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении выбранного жанра игры.");
        }
    }
}