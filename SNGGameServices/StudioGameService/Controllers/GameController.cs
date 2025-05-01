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

        public GameController(IGameService gameService, IMapper mapper)
        {
            this.gameService = gameService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех игр
        /// </summary>
        /// <returns>Список всех игр</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDTO>>> GetAllGames()
        {
            var games = await gameService.GetAllAsync();
            var gameDTOs = mapper.Map<IEnumerable<GameDTO>>(games);
            return Ok(gameDTOs);
        }

        /// <summary>
        /// Получение игры по ID
        /// </summary>
        /// <param name="id">Идентификатор игры</param>
        /// <returns>Игра с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDTO>> GetGameById(Guid id)
        {
            var game = await gameService.GetByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            var gameDTO = mapper.Map<GameDTO>(game);
            return Ok(gameDTO);
        }

        /// <summary>
        /// Создание новой игры
        /// </summary>
        /// <param name="gameCreateDTO">Данные для создания игры</param>
        /// <returns>Созданная игра</returns>
        [HttpPost]
        public async Task<ActionResult> CreateGame(GameDTO gameCreateDTO)
        {
            var game = mapper.Map<Game>(gameCreateDTO);
            await gameService.AddAsync(game);
            var gameResultDTO = mapper.Map<GameDTO>(game);
            return CreatedAtAction(nameof(GetGameById), new { id = game.Id }, gameResultDTO);
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
            if (id != gameDTO.Id)
            {
                return BadRequest();
            }

            var existingGame = await gameService.GetByIdAsync(id);
            if (existingGame == null)
            {
                return NotFound();
            }

            var game = mapper.Map<Game>(gameDTO);
            await gameService.UpdateAsync(game);
            return Ok(game);
        }

        /// <summary>
        /// Удаление игры
        /// </summary>
        /// <param name="id">Идентификатор игры</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGame(Guid id)
        {
            await gameService.DeleteAsync(id);
            return NoContent();
        }

        /// <summary> Получение списка фильтрованных игр
        /// </summary>
        /// <param name="paramFilter">параметры фильтрации</param>
        /// <returns>Список игр</returns>
        [HttpPost]
        public async Task<ActionResult> GetFilterGame([FromBody] ParamQueryGame paramFilter)
        {
            var games = await gameService.FilterGame(paramFilter);
            return Ok(games);
        }

        /// <summary> Получение списка статистики игр
        /// </summary>
        /// <param name="listGameId">параметры получения статистики игр</param>
        /// <returns>Список статистики игр</returns>
        [HttpPost]
        public async Task<ActionResult> GetStatisticGames([FromBody] List<Guid> listGameId)
        {
            var games = await gameService.GetStatisticGames(listGameId);

            var resultListStatistic = mapper.Map<IEnumerable<StatisticGameDTO>>(games);

            return Ok(resultListStatistic);
        }
    }
}
