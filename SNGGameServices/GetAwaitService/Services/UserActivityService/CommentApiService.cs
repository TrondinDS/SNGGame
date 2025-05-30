using GetAwaitService.Services.UserActivityService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using System.Text.Json;
using System.Text;

namespace GetAwaitService.Services.UserActivityService
{
    public class CommentApiService : ICommentApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CommentApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UserActivityServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<CommentDTO>?> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/Comment/GetAllComments");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<CommentDTO>>(body, _jsonOptions);
        }

        public async Task<CommentDTO?> GetByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Comment/GetCommentById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CommentDTO>(body, _jsonOptions);
        }

        public async Task<CommentDTO?> CreateAsync(CommentDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Comment/CreateComment", content);
            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CommentDTO>(responseBody, _jsonOptions);
        }

        public async Task<bool> UpdateAsync(Guid id, CommentDTO dto)
        {
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Comment/UpdateComment/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Comment/DeleteComment/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
