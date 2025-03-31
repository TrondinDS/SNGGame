using GetAwaitService.DB.DTO.UserActivityService.Topic;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.UserActivity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TopicController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserActivityServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            var response = await _httpClient.GetAsync("api/Topic/GetAllTopics");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var topics = JsonSerializer.Deserialize<IEnumerable<TopicDTO>>(responseBody, _jsonOptions);
                return Ok(topics);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка тем.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopicById(int id)
        {
            var response = await _httpClient.GetAsync($"api/Topic/GetTopicById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var topic = JsonSerializer.Deserialize<TopicDTO>(responseBody, _jsonOptions);
                return Ok(topic);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении темы по ID.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] TopicDTO topicDto)
        {
            var jsonContent = JsonSerializer.Serialize(topicDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Topic/CreateTopic", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdTopic = JsonSerializer.Deserialize<TopicDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetTopicById), new { id = createdTopic.Id }, createdTopic);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании темы.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic(int id, [FromBody] TopicDTO topicDto)
        {
            if (id != topicDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var jsonContent = JsonSerializer.Serialize(topicDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Topic/UpdateTopic/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении темы.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Topic/DeleteTopic/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении темы.");
        }
    }
}