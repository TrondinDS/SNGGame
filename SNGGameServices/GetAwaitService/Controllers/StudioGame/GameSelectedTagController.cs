using AutoMapper;
using GetAwaitService.Services.StudioGameService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedGenre;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameSelectedTag;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameSelectedTagController : ControllerBase
    {
        private readonly IGameSelectedTagService _service;
        private readonly IUserAccessRightsService _serviceUserRight;
        private readonly IMapper _mapper;

        public GameSelectedTagController(
            IGameSelectedTagService service,
            IUserAccessRightsService serviceUserRight,
            IMapper mapper)
        {
            _service = service;
            _serviceUserRight = serviceUserRight;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGameSelectedTags()
        {
            var result = await _service.GetAllAsync();
            return result != null ? Ok(result) : StatusCode(500, "Ошибка при получении связей 'Игра-Тег'.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameSelectedTagById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameSelectedTag([FromBody] GameSelectedTagCreateDTO dtoC)
        {

            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            var checkUserRights = await _serviceUserRight.ChekUserRightsModerAndAdminGameAsync(userId, dtoC.GameId);

            if (checkUserRights)
            {
                var dto = _mapper.Map<GameSelectedTagDTO>(dtoC);

                var created = await _service.CreateAsync(dto);
                return created != null
                    ? CreatedAtAction(nameof(GetGameSelectedTagById), new { id = created.Id }, created)
                    : StatusCode(500, "Ошибка при создании связи 'Игра-Тег'.");
            }
            return BadRequest("у вас недостаточно прав для выполения данного действия");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameSelectedTag(Guid id, [FromBody] GameSelectedTagDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID в пути не совпадает с ID в теле запроса.");

            var updated = await _service.UpdateAsync(dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameSelectedTag(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}