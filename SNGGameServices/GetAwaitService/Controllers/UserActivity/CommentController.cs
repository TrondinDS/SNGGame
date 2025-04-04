using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.UserActivity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserActivityServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var response = await _httpClient.GetAsync("api/Comment/GetAllComments");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var comments = JsonSerializer.Deserialize<IEnumerable<CommentDTO>>(responseBody, _jsonOptions);
                return Ok(comments);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка комментариев.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var response = await _httpClient.GetAsync($"api/Comment/GetCommentById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var comment = JsonSerializer.Deserialize<CommentDTO>(responseBody, _jsonOptions);
                return Ok(comment);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении комментария по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDTO commentDto)
        {
            var jsonContent = JsonSerializer.Serialize(commentDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Comment/CreateComment", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdComment = JsonSerializer.Deserialize<CommentDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetCommentById), new { id = createdComment.Id }, createdComment);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании комментария.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentDTO commentDto)
        {
            if (id != commentDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(commentDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Comment/UpdateComment/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении комментария.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Comment/DeleteComment/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении комментария.");
        }
    }
}