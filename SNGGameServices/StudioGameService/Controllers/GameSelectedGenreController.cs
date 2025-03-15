using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.DB.DTO.GameSelectedGenre;
using StudioGameService.DB.Model;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameSelectedGenreController : ControllerBase
    {
        private readonly IGameSelectedGenreService gameSelectedGenreService;
        private readonly IMapper mapper;

        public GameSelectedGenreController(
            IGameSelectedGenreService gameSelectedGenreService,
            IMapper mapper
        )
        {
            this.gameSelectedGenreService = gameSelectedGenreService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех связей "Игра-Жанр"
        /// </summary>
        /// <returns>Список всех связей "Игра-Жанр"</returns>
        [HttpGet]
        public async Task<
            ActionResult<IEnumerable<GameSelectedGenreDTO>>
        > GetAllGameSelectedGenres()
        {
            var gameSelectedGenres = await gameSelectedGenreService.GetAllAsync();
            var gameSelectedGenreDTOs = mapper.Map<IEnumerable<GameSelectedGenreDTO>>(
                gameSelectedGenres
            );
            return Ok(gameSelectedGenreDTOs);
        }

        /// <summary>
        /// Получение связи "Игра-Жанр" по ID
        /// </summary>
        /// <param name="id">Идентификатор связи</param>
        /// <returns>Связь "Игра-Жанр" с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSelectedGenreDTO>> GetGameSelectedGenreById(int id)
        {
            var gameSelectedGenre = await gameSelectedGenreService.GetByIdAsync(id);
            if (gameSelectedGenre == null)
            {
                return NotFound();
            }
            var gameSelectedGenreDTO = mapper.Map<GameSelectedGenreDTO>(gameSelectedGenre);
            return Ok(gameSelectedGenreDTO);
        }

        /// <summary>
        /// Создание новой связи "Игра-Жанр"
        /// </summary>
        /// <param name="gameSelectedGenreCreateDTO">Данные для создания связи</param>
        /// <returns>Созданная связь</returns>
        [HttpPost]
        public async Task<ActionResult> CreateGameSelectedGenre(
            GameSelectedGenreDTO gameSelectedGenreCreateDTO
        )
        {
            var gameSelectedGenre = mapper.Map<GameSelectedGenre>(gameSelectedGenreCreateDTO);
            await gameSelectedGenreService.AddAsync(gameSelectedGenre);
            var gameSelectedGenreResultDTO = mapper.Map<GameSelectedGenreDTO>(gameSelectedGenre);
            return CreatedAtAction(
                nameof(GetGameSelectedGenreById),
                new { id = gameSelectedGenre.Id },
                gameSelectedGenreResultDTO
            );
        }

        /// <summary>
        /// Обновление связи "Игра-Жанр"
        /// </summary>
        /// <param name="id">Идентификатор связи</param>
        /// <param name="gameSelectedGenreDTO">Обновленные данные связи</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameSelectedGenre(
            int id,
            GameSelectedGenreDTO gameSelectedGenreDTO
        )
        {
            if (id != gameSelectedGenreDTO.Id)
            {
                return BadRequest();
            }

            var existingGameSelectedGenre = await gameSelectedGenreService.GetByIdAsync(id);
            if (existingGameSelectedGenre == null)
            {
                return NotFound();
            }

            var gameSelectedGenre = mapper.Map<GameSelectedGenre>(gameSelectedGenreDTO);
            await gameSelectedGenreService.UpdateAsync(gameSelectedGenre);
            return Ok(gameSelectedGenre);
        }

        /// <summary>
        /// Удаление связи "Игра-Жанр"
        /// </summary>
        /// <param name="id">Идентификатор связи</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameSelectedGenre(int id)
        {
            await gameSelectedGenreService.DeleteAsync(id);
            return NoContent();
        }
    }
}
