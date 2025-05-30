using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Job;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.UserService
{
    public class JobApiService : IJobApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public JobApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<JobDTO>?> GetAllJobAsync()
        {
            var response = await _httpClient.GetAsync("api/Job/GetAllJob");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<JobDTO>>(content, _jsonOptions);
        }

        public async Task<JobDTO?> GetJobByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Job/GetJobById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<JobDTO>(content, _jsonOptions);
        }

        public async Task<JobDTO?> CreateJobAsync(JobCreateDTO jobDto)
        {
            var json = JsonSerializer.Serialize(jobDto);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Job/CreateJob", httpContent);
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<JobDTO>(content, _jsonOptions);
        }

        public async Task<bool> UpdateJobAsync(Guid id, JobDTO jobDto)
        {
            var json = JsonSerializer.Serialize(jobDto);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Job/UpdateJob/{id}", httpContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteJobAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Job/DeleteJob/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
