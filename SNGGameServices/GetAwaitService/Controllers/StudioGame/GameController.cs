using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame;
using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GameController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var response = await _httpClient.GetAsync("api/Game/GetAllGames");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var games = JsonSerializer.Deserialize<IEnumerable<GameDTO>>(responseBody, _jsonOptions);
                return Ok(games);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка игр.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Game/GetGameById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var game = JsonSerializer.Deserialize<GameDTO>(responseBody, _jsonOptions);
                return Ok(game);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении игры по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] GameDTO gameDto)
        {
            var jsonContent = JsonSerializer.Serialize(gameDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Game/CreateGame", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdGame = JsonSerializer.Deserialize<GameDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetGameById), new { id = createdGame.Id }, createdGame);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании игры.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(Guid id, [FromBody] GameDTO gameDto)
        {
            if (id != gameDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(gameDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Game/UpdateGame/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении игры.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Game/DeleteGame/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении игры.");
        }

        [HttpPost]
        public async Task<IActionResult> GetFilterGame([FromBody] ParamQueryGame paramFilter)
        {
            // Сериализация параметров фильтрации в JSON
            var jsonContent = JsonSerializer.Serialize(paramFilter);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Вызов метода фильтрации через HTTP POST-запрос
            var response = await _httpClient.PostAsync("api/Game/GetFilterGame", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var filteredGames = JsonSerializer.Deserialize<IEnumerable<GameDTO>>(responseBody, _jsonOptions);
                return Ok(filteredGames);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении фильтрованных игр.");
        }

        [HttpPost]
        public async Task<ActionResult> GetStatisticGames([FromBody] List<Guid> listGameId)
        {
            // Сериализация параметров фильтрации в JSON
            var jsonContent = JsonSerializer.Serialize(listGameId);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Вызов метода фильтрации через HTTP POST-запрос
            var response = await _httpClient.PostAsync("api/Game/GetStatisticGames", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<IEnumerable<StatisticGameDTO>>(responseBody, _jsonOptions);
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении статистики игр.");
        }
    }
}