using Library.Generics.DB.DTO.DTOModelObjects.Game;
using Library.Generics.DB.DTO.DTOModelObjects.Studio;
using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CardController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("StudioGameServiceClient");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр
            };
        }

        /// <summary>
        /// Получение всех карточек игр
        /// </summary>
        /// <returns>Список всех карточек игр</returns>
        [Authorize(Roles = "user")]
        [HttpGet]
        public async Task<IActionResult> GetAllCardGames()
        {
            var response = await _httpClient.GetAsync("api/Card/GetAllCardGames");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var cardGames = JsonSerializer.Deserialize<IEnumerable<CardGameDTO>>(responseBody, _jsonOptions);
                return Ok(cardGames);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении списка карточек игр.");
        }

        /// <summary>
        /// Получение выбранных карточек игр
        /// </summary>
        /// <param name="idGames">Список ID игр</param>
        /// <returns>Список выбранных карточек игр</returns>
        [HttpPost]
        public async Task<IActionResult> GetSelectCardGames([FromBody] List<Guid> idGames)
        {
            var jsonContent = JsonSerializer.Serialize(idGames);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Card/GetSelectCardGames", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var cardGames = JsonSerializer.Deserialize<IEnumerable<CardGameDTO>>(responseBody, _jsonOptions);
                return Ok(cardGames);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении выбранных карточек игр.");
        }

        /// <summary>
        /// Получение карточек игр с помощью фильтра
        /// </summary>
        /// <param name="paramQuery">Параметры фильтрации</param>
        /// <returns>Список карточек игр с помощью фильтра</returns>
        [HttpPost]
        public async Task<IActionResult> GetFiltreCardGames([FromBody] ParamQueryGame paramQuery)
        {
            var jsonContent = JsonSerializer.Serialize(paramQuery);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Card/GetFiltreCardGames", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var cardGames = JsonSerializer.Deserialize<IEnumerable<CardGameDTO>>(responseBody, _jsonOptions);
                return Ok(cardGames);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении отфильтрованных карточек игр.");
        }

        /// <summary>
        /// Получение карточек студий с помощью фильтрации (поиска по id)
        /// </summary>
        /// <param name="ParamQueryStudio">Параметры фильтрации</param>
        /// <returns>Список карточек игр с помощью фильтра</returns>
        [HttpPost]
        public async Task<IActionResult> GetSearchCardStudio([FromBody] ParamQueryStudio paramQueryList)
        {
            var jsonContent = JsonSerializer.Serialize(paramQueryList);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Card/GetSearchCardStudio", httpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var cardGames = JsonSerializer.Deserialize<IEnumerable<CardStudioDTO>>(responseBody, _jsonOptions);
                return Ok(cardGames);
            }

            return StatusCode((int)response.StatusCode, "Ошибка при получении отфильтрованных карточек игр.");
        }
    }
}