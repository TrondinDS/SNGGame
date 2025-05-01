using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BannedController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public BannedController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBanned()
        {
            var response = await _httpClient.GetAsync("api/Banned/GetAllBanned");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var banneds = JsonSerializer.Deserialize<IEnumerable<BannedDTO>>(responseBody, _jsonOptions);
                return Ok(banneds);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка банов");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannedById(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Banned/GetBannedById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var banned = JsonSerializer.Deserialize<BannedDTO>(responseBody, _jsonOptions);
                return Ok(banned);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении бана по ID");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanned([FromBody] BannedDTO bannedDto)
        {
            var jsonContent = JsonSerializer.Serialize(bannedDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Banned/CreateBanned", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdBanned = JsonSerializer.Deserialize<BannedDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetBannedById), new { id = createdBanned.Id }, createdBanned);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании бана");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanned(Guid id, [FromBody] BannedDTO bannedDto)
        {
            if (id != bannedDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных");

            var jsonContent = JsonSerializer.Serialize(bannedDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Banned/UpdateBanned/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении бана");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanned(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Banned/DeleteBanned/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении бана");
        }
    }
}