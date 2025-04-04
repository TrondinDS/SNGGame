using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public GenreController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var response = await _httpClient.GetAsync("api/Genre/GetAllGenres");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var genres = JsonSerializer.Deserialize<IEnumerable<GenreDTO>>(responseBody, _jsonOptions);
                return Ok(genres);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка жанров.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var response = await _httpClient.GetAsync($"api/Genre/GetGenreById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var genre = JsonSerializer.Deserialize<GenreDTO>(responseBody, _jsonOptions);
                return Ok(genre);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении жанра по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDTO genreDto)
        {
            var jsonContent = JsonSerializer.Serialize(genreDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Genre/CreateGenre", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdGenre = JsonSerializer.Deserialize<GenreDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetGenreById), new { id = createdGenre.Id }, createdGenre);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании жанра.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreDTO genreDto)
        {
            if (id != genreDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(genreDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Genre/UpdateGenre/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении жанра.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Genre/DeleteGenre/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении жанра.");
        }
    }
}