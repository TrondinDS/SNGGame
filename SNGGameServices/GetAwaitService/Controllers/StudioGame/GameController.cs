using AutoMapper;
using GetAwaitService.Services.StudioGameService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IUserAccessRightsService _userAccessRightsService;
        private readonly IMapper _mapper;

        public GameController(
            IGameService gameService, 
            IUserAccessRightsService userAccessRightsService, 
            IMapper mapper)
        {
            _gameService = gameService;
            _userAccessRightsService = userAccessRightsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            var games = await _gameService.GetAllAsync();
            return games != null ? Ok(games) : StatusCode(500, "Ошибка при получении списка игр.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameById(Guid id)
        {
            var game = await _gameService.GetByIdAsync(id);
            return game != null ? Ok(game) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] GameCreateDTO gameDtoC)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            var checkUserRights = await _userAccessRightsService
                .ChekUserRightsModerAndAdminStudioAsync(userId, gameDtoC.StudioId);
            if (checkUserRights)
            {
                var gameDto = _mapper.Map<GameDTO>(gameDtoC);

                var created = await _gameService.CreateAsync(gameDto);
                return created != null
                    ? CreatedAtAction(nameof(GetGameById), new { id = created.Id }, created)
                    : StatusCode(500, "Ошибка при создании игры.");
            }

            return BadRequest("у вас недостаточно прав для выполения данного действия");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(Guid id, [FromBody] GameDTO gameDto)
        {
            if (id != gameDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            var checkUserRights = await _userAccessRightsService.ChekUserRightsModerAndAdminGameAsync(userId, id);

            if (checkUserRights)
            {
                var updated = await _gameService.UpdateAsync(id, gameDto);
                return updated ? Ok() : NotFound();
            }

            return BadRequest("у вас недостаточно прав для выполения данного действия");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            var deleted = await _gameService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> GetFilterGame([FromBody] ParamQueryGame query)
        {
            var result = await _gameService.GetFilteredAsync(query);
            return result != null ? Ok(result) : StatusCode(500, "Ошибка при фильтрации игр.");
        }

        [HttpPost]
        public async Task<IActionResult> GetStatisticGamesByIdGames([FromBody] List<Guid> ids)
        {
            var stats = await _gameService.GetStatisticsAsync(ids);
            return stats != null ? Ok(stats) : StatusCode(500, "Ошибка при получении статистики.");
        }

        [HttpPost]
        public async Task<IActionResult> GetGameDTOViewByIdGames([FromBody] List<Guid> ids)
        {
            var stats = await _gameService.GetGameDTOViewByIdGamesAsync(ids);
            return stats != null ? Ok(stats) : StatusCode(500, "Ошибка при получении статистики.");
        }
    }
}