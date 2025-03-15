using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.DB.DTO.Genre;
using StudioGameService.DB.Model;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;
        private readonly IMapper mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            this.genreService = genreService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех жанров
        /// </summary>
        /// <returns>Список всех жанров</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetAllGenres()
        {
            var genres = await genreService.GetAllAsync();
            var genreDTOs = mapper.Map<IEnumerable<GenreDTO>>(genres);
            return Ok(genreDTOs);
        }

        /// <summary>
        /// Получение жанра по ID
        /// </summary>
        /// <param name="id">Идентификатор жанра</param>
        /// <returns>Жанр с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDTO>> GetGenreById(int id)
        {
            var genre = await genreService.GetByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            var genreDTO = mapper.Map<GenreDTO>(genre);
            return Ok(genreDTO);
        }

        /// <summary>
        /// Создание нового жанра
        /// </summary>
        /// <param name="genreCreateDTO">Данные для создания жанра</param>
        /// <returns>Созданный жанр</returns>
        [HttpPost]
        public async Task<ActionResult> CreateGenre(GenreDTO genreCreateDTO)
        {
            var genre = mapper.Map<Genre>(genreCreateDTO);
            await genreService.AddAsync(genre);
            var genreResultDTO = mapper.Map<GenreDTO>(genre);
            return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genreResultDTO);
        }

        /// <summary>
        /// Обновление жанра
        /// </summary>
        /// <param name="id">Идентификатор жанра</param>
        /// <param name="genreDTO">Обновленные данные жанра</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGenre(int id, GenreDTO genreDTO)
        {
            if (id != genreDTO.Id)
            {
                return BadRequest();
            }

            var existingGenre = await genreService.GetByIdAsync(id);
            if (existingGenre == null)
            {
                return NotFound();
            }

            var genre = mapper.Map<Genre>(genreDTO);
            await genreService.UpdateAsync(genre);
            return Ok(genre);
        }

        /// <summary>
        /// Удаление жанра
        /// </summary>
        /// <param name="id">Идентификатор жанра</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenre(int id)
        {
            await genreService.DeleteAsync(id);
            return NoContent();
        }
    }
}
