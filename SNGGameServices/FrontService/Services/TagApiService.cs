using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;
using System.Text.Json;
using System.Text;

namespace FrontService.Services
{
    public class TagApiService : ITagApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TagApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GetAwaitClient");
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<TagDTO>?> GetAllTagsAsync()
        {
            var response = await _httpClient.GetAsync("api/Tag/GetAllTags");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<TagDTO>>(content, _jsonOptions);
        }

        public async Task<TagDTO?> GetTagByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Tag/GetTagById/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TagDTO>(content, _jsonOptions);
        }

        public async Task<TagDTO?> CreateTagAsync(TagCreateDTO tagDto)
        {
            var content = new StringContent(JsonSerializer.Serialize(tagDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Tag/CreateTag", content);

            if (!response.IsSuccessStatusCode) return null;

            var body = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TagDTO>(body, _jsonOptions);
        }

        public async Task<bool> UpdateTagAsync(Guid id, TagDTO tagDto)
        {
            if (id != tagDto.Id) return false;

            var content = new StringContent(JsonSerializer.Serialize(tagDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Tag/UpdateTag/{id}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTagAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Tag/DeleteTag/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
