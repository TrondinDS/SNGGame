using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.UserReaction;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.UserActivity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserReactionController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserReactionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserActivityServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReactions()
        {
            var response = await _httpClient.GetAsync("api/UserReaction/GetAllReactions");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var reactions = JsonSerializer.Deserialize<IEnumerable<UserReactionDTO>>(responseBody, _jsonOptions);
                return Ok(reactions);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка реакций.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReactionById(int id)
        {
            var response = await _httpClient.GetAsync($"api/UserReaction/GetReactionById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var reaction = JsonSerializer.Deserialize<UserReactionDTO>(responseBody, _jsonOptions);
                return Ok(reaction);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении реакции по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateReaction([FromBody] UserReactionDTO reactionDto)
        {
            var jsonContent = JsonSerializer.Serialize(reactionDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/UserReaction/CreateReaction", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdReaction = JsonSerializer.Deserialize<UserReactionDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetReactionById), new { id = createdReaction.Id }, createdReaction);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании реакции.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReaction(int id, [FromBody] UserReactionDTO reactionDto)
        {
            if (id != reactionDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(reactionDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/UserReaction/UpdateReaction/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении реакции.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReaction(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/UserReaction/DeleteReaction/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении реакции.");
        }
    }
}