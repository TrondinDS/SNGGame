using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public StudioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudios()
        {
            var response = await _httpClient.GetAsync("api/Studio/GetAllStudios");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var studios = JsonSerializer.Deserialize<IEnumerable<StudioDTO>>(responseBody, _jsonOptions);
                return Ok(studios);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка студий.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudioById(int id)
        {
            var response = await _httpClient.GetAsync($"api/Studio/GetStudioById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var studio = JsonSerializer.Deserialize<StudioDTO>(responseBody, _jsonOptions);
                return Ok(studio);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении студии по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudio([FromBody] StudioDTO studioDto)
        {
            var jsonContent = JsonSerializer.Serialize(studioDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Studio/CreateStudio", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdStudio = JsonSerializer.Deserialize<StudioDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetStudioById), new { id = createdStudio.Id }, createdStudio);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании студии.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudio(int id, [FromBody] StudioDTO studioDto)
        {
            if (id != studioDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(studioDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Studio/UpdateStudio/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении студии.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudio(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Studio/DeleteStudio/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении студии.");
        }
    }
}