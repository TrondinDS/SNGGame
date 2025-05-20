using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame;
using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.DB.Model;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;
        private readonly IMapper mapper;
        private readonly ILogger<GameController> logger;

        public GameController(IGameService gameService, IMapper mapper, ILogger<GameController> logger)
        {
            this.gameService = gameService;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Получение всех игр
        /// </summary>
        /// <returns>Список всех игр</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames()
        {
            try
            {
                var result = await gameService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при получении всех игр");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Получение игры по ID
        /// </summary>
        /// <param name="id">Идентификатор игры</param>
        /// <returns>Игра с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGameById(Guid id)
        {
            try
            {
                var result = await gameService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при получении игры по ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Создание новой игры
        /// </summary>
        /// <param name="gameCreateDTO">Данные для создания игры</param>
        /// <returns>Созданная игра</returns>
        [HttpPost]
        public async Task<ActionResult> CreateGame(GameDTO game)
        {
            try
            {
                await gameService.AddAsync(game);
                return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, game);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при создании игры");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Обновление игры
        /// </summary>
        /// <param name="id">Идентификатор игры</param>
        /// <param name="gameDTO">Обновленные данные игры</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGame(Guid id, GameDTO gameDTO)
        {
            try
            {
                if (id != gameDTO.Id)
                {
                    return BadRequest();
                }

                var existingGame = await gameService.GetByIdAsync(id);
                if (existingGame == null)
                {
                    return NotFound();
                }

                await gameService.UpdateAsync(gameDTO);
                return Ok(gameDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при обновлении игры с ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Удаление игры
        /// </summary>
        /// <param name="id">Идентификатор игры</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(Guid id)
        {
            try
            {
                await gameService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при удалении игры с ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary> Получение списка фильтрованных игр
        /// </summary>
        /// <param name="paramFilter">параметры фильтрации</param>
        /// <returns>Список игр</returns>
        [HttpPost]
        public async Task<ActionResult> GetFilterGame([FromBody] ParamQueryGame paramFilter)
        {
            try
            {
                var games = await gameService.FilterGame(paramFilter);
                return Ok(games);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при фильтрации игр");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary> Получение списка статистики игр
        /// </summary>
        /// <param name="listGameId">параметры получения статистики игр</param>
        /// <returns>Список статистики игр</returns>
        [HttpPost]
        public async Task<ActionResult> GetStatisticGames([FromBody] List<Guid> listGameId)
        {
            try
            {
                var games = await gameService.GetStatisticGames(listGameId);
                var resultListStatistic = mapper.Map<IEnumerable<StatisticGameDTO>>(games);

                return Ok(resultListStatistic);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при получении статистики игр");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }
    }
}
