using GetAwaitService.DB.DTO.UserService.Job;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public JobController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJob()
        {
            var response = await _httpClient.GetAsync("api/Job/GetAllJob");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var jobs = JsonSerializer.Deserialize<IEnumerable<JobDTO>>(responseBody, _jsonOptions);
                return Ok(jobs);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка должностей");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var response = await _httpClient.GetAsync($"api/Job/GetJobById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var job = JsonSerializer.Deserialize<JobDTO>(responseBody, _jsonOptions);
                return Ok(job);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при получении должности по ID");
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] JobDTO jobDto)
        {
            var jsonContent = JsonSerializer.Serialize(jobDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Job/CreateJob", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var createdJob = JsonSerializer.Deserialize<JobDTO>(responseBody, _jsonOptions);
                return CreatedAtAction(nameof(GetJobById), new { id = createdJob.Id }, createdJob);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при создании должности");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] JobDTO jobDto)
        {
            if (id != jobDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных");

            var jsonContent = JsonSerializer.Serialize(jobDto);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Job/UpdateJob/{id}", httpContent);

            if (response.IsSuccessStatusCode)
                return Ok();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при обновлении должности");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Job/DeleteJob/{id}");

            if (response.IsSuccessStatusCode)
                return NoContent();

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return NotFound();

            return StatusCode((int)response.StatusCode, "Ошибка при удалении должности");
        }
    }
}