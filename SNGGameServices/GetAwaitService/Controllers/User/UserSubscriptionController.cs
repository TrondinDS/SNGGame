using GetAwaitService.DB.DTO.UserService.UserSubscription;
using GetAwaitService.DB.DTO.UserService.User;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UserSubscriptionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserSubscription()
        {
            var response = await _httpClient.GetAsync("api/UserSubscription/GetAllUserSubscription");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var subscriptions = JsonSerializer.Deserialize<IEnumerable<UserSubscriptionDTO>>(responseBody, _jsonOptions);
                return Ok(subscriptions);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении подписок пользователей");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserSubscriptionById(int id)
        {
            var response = await _httpClient.GetAsync($"api/UserSubscription/GetUserSubscriptionById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var subscription = JsonSerializer.Deserialize<UserSubscriptionDTO>(responseBody, _jsonOptions);
                return Ok(subscription);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении подписки пользователя");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserSubscription([FromBody] UserSubscriptionDTO subscriptionDto)
        {
            var jsonContent = JsonSerializer.Serialize(subscriptionDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/UserSubscription/CreateUserSubscription", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdSubscription = JsonSerializer.Deserialize<UserSubscriptionDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetUserSubscriptionById), new { id = createdSubscription.Id }, createdSubscription);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании подписки пользователя");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserSubscription(int id, [FromBody] UserSubscriptionDTO subscriptionDto)
        {
            if (id != subscriptionDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных");

            var jsonContent = JsonSerializer.Serialize(subscriptionDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/UserSubscription/UpdateUserSubscription/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении подписки пользователя");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSubscription(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/UserSubscription/DeleteUserSubscription/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении подписки пользователя");
        }
    }
}