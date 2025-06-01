using AutoMapper;
using GetAwaitService.Services.StudioGameService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.GameLibrary;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameLibraryController : ControllerBase
    {
        private readonly IGameLibraryService _libraryService;
        private readonly IMapper _mapper;

        public GameLibraryController(IGameLibraryService libraryService, IMapper mapper)
        {
            _libraryService = libraryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGameLibraries()
        {
            var libraries = await _libraryService.GetAllAsync();
            return libraries != null ? Ok(libraries) : StatusCode(500, "Ошибка при получении списка библиотек игр.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameLibraryById(Guid id)
        {
            var library = await _libraryService.GetByIdAsync(id);
            return library != null ? Ok(library) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGameLibrary([FromBody] GameLibraryCreateDTO dtoC)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            var dto = _mapper.Map<GameLibraryDTO>(dtoC);
            dto.UserId = userId;

            var created = await _libraryService.CreateAsync(dto);
            return created != null
                ? CreatedAtAction(nameof(GetGameLibraryById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании записи в библиотеке игр.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameLibrary(Guid id, [FromBody] GameLibraryDTO dto)
        {
            if (id != dto.Id) return BadRequest("ID в запросе не совпадает с ID в данных.");

            var success = await _libraryService.UpdateAsync(dto);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameLibrary(Guid id)
        {
            var success = await _libraryService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}