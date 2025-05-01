using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TagController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var response = await _httpClient.GetAsync("api/Tag/GetAllTags");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var tags = JsonSerializer.Deserialize<IEnumerable<TagDTO>>(responseBody, _jsonOptions);
                return Ok(tags);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка тегов.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Tag/GetTagById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var tag = JsonSerializer.Deserialize<TagDTO>(responseBody, _jsonOptions);
                return Ok(tag);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении тега по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] TagDTO tagDto)
        {
            var jsonContent = JsonSerializer.Serialize(tagDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Tag/CreateTag", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdTag = JsonSerializer.Deserialize<TagDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetTagById), new { id = createdTag.Id }, createdTag);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании тега.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(Guid id, [FromBody] TagDTO tagDto)
        {
            if (id != tagDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(tagDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Tag/UpdateTag/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении тега.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Tag/DeleteTag/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении тега.");
        }
    }
}