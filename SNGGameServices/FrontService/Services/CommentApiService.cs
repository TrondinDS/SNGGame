using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using System.Text.Json;
using System.Text;
using FrontService.Services.Interfaces;

namespace FrontService.Services
{
    namespace FrontService.Services.CommentService
    {
        /// <summary>
        /// Сервис для взаимодействия с API комментариев через HttpClient.
        /// </summary>
        public class CommentApiService : ICommentApiService
        {
            private readonly HttpClient _httpClient;
            private readonly JsonSerializerOptions _jsonOptions;

            public CommentApiService(IHttpClientFactory httpClientFactory)
            {
                _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
                _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            }

            /// <summary>
            /// Получить все комментарии.
            /// </summary>
            public async Task<IEnumerable<CommentDTO>?> GetAllCommentsAsync()
            {
                var response = await _httpClient.GetAsync("api/Comment/GetAllComments");
                if (!response.IsSuccessStatusCode) return null;

                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<CommentDTO>>(body, _jsonOptions);
            }

            /// <summary>
            /// Получить комментарий по идентификатору.
            /// </summary>
            public async Task<CommentDTO?> GetCommentByIdAsync(Guid id)
            {
                var response = await _httpClient.GetAsync($"api/Comment/GetCommentById/{id}");
                if (!response.IsSuccessStatusCode) return null;

                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CommentDTO>(body, _jsonOptions);
            }

            /// <summary>
            /// Создать новый комментарий.
            /// </summary>
            public async Task<CommentDTO?> CreateCommentAsync(CommentCreateDTO commentDto)
            {
                var content = new StringContent(JsonSerializer.Serialize(commentDto), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Comment/CreateComment", content);

                if (!response.IsSuccessStatusCode) return null;

                var body = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CommentDTO>(body, _jsonOptions);
            }

            /// <summary>
            /// Обновить существующий комментарий.
            /// </summary>
            public async Task<bool> UpdateCommentAsync(Guid id, CommentDTO commentDto)
            {
                if (id != commentDto.Id)
                    return false;

                var content = new StringContent(JsonSerializer.Serialize(commentDto), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"api/Comment/UpdateComment/{id}", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка при обновлении комментария. Статус: {response.StatusCode}, Детали: {errorContent}");
                }

                return response.IsSuccessStatusCode;
            }

            /// <summary>
            /// Удалить комментарий по идентификатору.
            /// </summary>
            public async Task<bool> DeleteCommentAsync(Guid id)
            {
                var response = await _httpClient.DeleteAsync($"api/Comment/DeleteComment/{id}");
                return response.IsSuccessStatusCode;
            }
        }
    }
}
