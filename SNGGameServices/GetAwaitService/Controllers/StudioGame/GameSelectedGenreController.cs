using AutoMapper;
using GetAwaitService.Services.StudioGameService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameSelectedGenreController : ControllerBase
    {
        private readonly IGameSelectedGenreService _serviceGameSelectedGenre;
        private readonly IUserAccessRightsService _serviceUserRight;
        private readonly IMapper _mapper;

        public GameSelectedGenreController(
            IGameSelectedGenreService serviceGameSelectedGenre, 
            IUserAccessRightsService serviceUserRight,
            IMapper mapper)
        {
            _serviceGameSelectedGenre = serviceGameSelectedGenre;
            _serviceUserRight = serviceUserRight;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGameSelectedGenres()
        {
            var genres = await _serviceGameSelectedGenre.GetAllAsync();
            return genres != null ? Ok(genres) : StatusCode(500, "Ошибка при получении списка выбранных жанров.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameSelectedGenreById(Guid id)
        {
            var genre = await _serviceGameSelectedGenre.GetByIdAsync(id);
            return genre != null ? Ok(genre) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameSelectedGenre([FromBody] GameSelectedGenreCreateDTO dtoC)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            var checkUserRights = await _serviceUserRight.CheckUserRightsModerAndAdminGameAsync(userId, dtoC.GameId);

            if (checkUserRights)
            {
                var dto = _mapper.Map<GameSelectedGenreDTO>(dtoC);

                var created = await _serviceGameSelectedGenre.CreateAsync(dto);
                return created != null
                    ? CreatedAtAction(nameof(GetGameSelectedGenreById), new { id = created.Id }, created)
                    : StatusCode(500, "Ошибка при создании выбранного жанра.");
            }
            return BadRequest("у вас недостаточно прав для выполения данного действия");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameSelectedGenre(Guid id, [FromBody] GameSelectedGenreDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID в пути не совпадает с ID в теле запроса.");

            var updated = await _serviceGameSelectedGenre.UpdateAsync(dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameSelectedGenre(Guid id)
        {
            var deleted = await _serviceGameSelectedGenre.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}