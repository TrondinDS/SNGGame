using GetAwaitService.Services.StudioGameService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameSelectedGenreController : ControllerBase
    {
        private readonly IGameSelectedGenreService _service;

        public GameSelectedGenreController(IGameSelectedGenreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGameSelectedGenres()
        {
            var genres = await _service.GetAllAsync();
            return genres != null ? Ok(genres) : StatusCode(500, "Ошибка при получении списка выбранных жанров.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameSelectedGenreById(Guid id)
        {
            var genre = await _service.GetByIdAsync(id);
            return genre != null ? Ok(genre) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameSelectedGenre([FromBody] GameSelectedGenreDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return created != null
                ? CreatedAtAction(nameof(GetGameSelectedGenreById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании выбранного жанра.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameSelectedGenre(Guid id, [FromBody] GameSelectedGenreDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID в пути не совпадает с ID в теле запроса.");

            var updated = await _service.UpdateAsync(dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameSelectedGenre(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}