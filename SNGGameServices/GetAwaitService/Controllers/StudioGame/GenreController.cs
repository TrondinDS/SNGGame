using GetAwaitService.Services.StudioGameService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenreController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var result = await _service.GetAllAsync();
            return result != null ? Ok(result) : StatusCode(500, "Ошибка при получении жанров.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(Guid id)
        {
            var genre = await _service.GetByIdAsync(id);
            return genre != null ? Ok(genre) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return created != null
                ? CreatedAtAction(nameof(GetGenreById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании жанра.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(Guid id, [FromBody] GenreDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var updated = await _service.UpdateAsync(dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}