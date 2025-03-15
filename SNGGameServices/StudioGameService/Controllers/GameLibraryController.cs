using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.DB.DTO.GameLibrary;
using StudioGameService.DB.Model;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameLibraryController : ControllerBase
    {
        private readonly IGameLibraryService gameLibraryService;
        private readonly IMapper mapper;

        public GameLibraryController(IGameLibraryService gameLibraryService, IMapper mapper)
        {
            this.gameLibraryService = gameLibraryService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех записей библиотеки игр
        /// </summary>
        /// <returns>Список всех записей библиотеки игр</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameLibraryDTO>>> GetAllGameLibraries()
        {
            var gameLibraries = await gameLibraryService.GetAllAsync();
            var gameLibraryDTOs = mapper.Map<IEnumerable<GameLibraryDTO>>(gameLibraries);
            return Ok(gameLibraryDTOs);
        }

        /// <summary>
        /// Получение записи библиотеки игр по ID
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns>Запись библиотеки игр с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameLibraryDTO>> GetGameLibraryById(int id)
        {
            var gameLibrary = await gameLibraryService.GetByIdAsync(id);
            if (gameLibrary == null)
            {
                return NotFound();
            }
            var gameLibraryDTO = mapper.Map<GameLibraryDTO>(gameLibrary);
            return Ok(gameLibraryDTO);
        }

        /// <summary>
        /// Создание новой записи в библиотеке игр
        /// </summary>
        /// <param name="gameLibraryCreateDTO">Данные для создания записи</param>
        /// <returns>Созданная запись</returns>
        [HttpPost]
        public async Task<ActionResult> CreateGameLibrary(GameLibraryDTO gameLibraryCreateDTO)
        {
            var gameLibrary = mapper.Map<GameLibrary>(gameLibraryCreateDTO);
            await gameLibraryService.AddAsync(gameLibrary);
            var gameLibraryResultDTO = mapper.Map<GameLibraryDTO>(gameLibrary);
            return CreatedAtAction(
                nameof(GetGameLibraryById),
                new { id = gameLibrary.Id },
                gameLibraryResultDTO
            );
        }

        /// <summary>
        /// Обновление записи в библиотеке игр
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <param name="gameLibraryDTO">Обновленные данные записи</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameLibrary(int id, GameLibraryDTO gameLibraryDTO)
        {
            if (id != gameLibraryDTO.Id)
            {
                return BadRequest();
            }

            var existingGameLibrary = await gameLibraryService.GetByIdAsync(id);
            if (existingGameLibrary == null)
            {
                return NotFound();
            }

            var gameLibrary = mapper.Map<GameLibrary>(gameLibraryDTO);
            await gameLibraryService.UpdateAsync(gameLibrary);
            return Ok(gameLibrary);
        }

        /// <summary>
        /// Удаление записи из библиотеки игр
        /// </summary>
        /// <param name="id">Идентификатор записи</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameLibrary(int id)
        {
            await gameLibraryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
