using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.DB.DTO.GameSelectedTag;
using StudioGameService.DB.Model;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameSelectedTagController : ControllerBase
    {
        private readonly IGameSelectedTagService gameSelectedTagService;
        private readonly IMapper mapper;

        public GameSelectedTagController(
            IGameSelectedTagService gameSelectedTagService,
            IMapper mapper
        )
        {
            this.gameSelectedTagService = gameSelectedTagService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех связей "Игра-Тег"
        /// </summary>
        /// <returns>Список всех связей "Игра-Тег"</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameSelectedTagDTO>>> GetAllGameSelectedTags()
        {
            var gameSelectedTags = await gameSelectedTagService.GetAllAsync();
            var gameSelectedTagDTOs = mapper.Map<IEnumerable<GameSelectedTagDTO>>(gameSelectedTags);
            return Ok(gameSelectedTagDTOs);
        }

        /// <summary>
        /// Получение связи "Игра-Тег" по ID
        /// </summary>
        /// <param name="id">Идентификатор связи</param>
        /// <returns>Связь "Игра-Тег" с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameSelectedTagDTO>> GetGameSelectedTagById(int id)
        {
            var gameSelectedTag = await gameSelectedTagService.GetByIdAsync(id);
            if (gameSelectedTag == null)
            {
                return NotFound();
            }
            var gameSelectedTagDTO = mapper.Map<GameSelectedTagDTO>(gameSelectedTag);
            return Ok(gameSelectedTagDTO);
        }

        /// <summary>
        /// Создание новой связи "Игра-Тег"
        /// </summary>
        /// <param name="gameSelectedTagCreateDTO">Данные для создания связи</param>
        /// <returns>Созданная связь</returns>
        [HttpPost]
        public async Task<ActionResult> CreateGameSelectedTag(
            GameSelectedTagDTO gameSelectedTagCreateDTO
        )
        {
            var gameSelectedTag = mapper.Map<GameSelectedTag>(gameSelectedTagCreateDTO);
            await gameSelectedTagService.AddAsync(gameSelectedTag);
            var gameSelectedTagResultDTO = mapper.Map<GameSelectedTagDTO>(gameSelectedTag);
            return CreatedAtAction(
                nameof(GetGameSelectedTagById),
                new { id = gameSelectedTag.Id },
                gameSelectedTagResultDTO
            );
        }

        /// <summary>
        /// Обновление связи "Игра-Тег"
        /// </summary>
        /// <param name="id">Идентификатор связи</param>
        /// <param name="gameSelectedTagDTO">Обновленные данные связи</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameSelectedTag(
            int id,
            GameSelectedTagDTO gameSelectedTagDTO
        )
        {
            if (id != gameSelectedTagDTO.Id)
            {
                return BadRequest();
            }

            var existingGameSelectedTag = await gameSelectedTagService.GetByIdAsync(id);
            if (existingGameSelectedTag == null)
            {
                return NotFound();
            }

            var gameSelectedTag = mapper.Map<GameSelectedTag>(gameSelectedTagDTO);
            await gameSelectedTagService.UpdateAsync(gameSelectedTag);
            return Ok(gameSelectedTag);
        }

        /// <summary>
        /// Удаление связи "Игра-Тег"
        /// </summary>
        /// <param name="id">Идентификатор связи</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameSelectedTag(int id)
        {
            await gameSelectedTagService.DeleteAsync(id);
            return NoContent();
        }
    }
}
