using AutoMapper;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BannedController : ControllerBase
    {
        private readonly IBannedApiService _bannedService;
        private readonly IUserAccessRightsService _userAccessRightsService;
        private readonly IMapper _mapper;
        public BannedController(
            IBannedApiService bannedService,
            IUserAccessRightsService userAccessRightsService,
            IMapper mapper)
        {
            _bannedService = bannedService;
            _userAccessRightsService = userAccessRightsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBanned()
        {
            var result = await _bannedService.GetAllAsync();
            return result != null ? Ok(result) : StatusCode(500, "Ошибка при получении списка банов");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannedsByUserId(Guid id)
        {
            var result = await _bannedService.GetBannedsByUserId(id);
            return result != null ? Ok(result) : StatusCode(500, "Ошибка при получении списка банов пользователя");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannedById(Guid id)
        {
            var result = await _bannedService.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanned([FromBody] BannedCreateDTO bannedDtoC)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            var checkUserRights = await _userAccessRightsService
                .ChekUserRightsModerAndAdminBanGlobalAndLocalAsync(userId, bannedDtoC);

            if (checkUserRights)
            {
                var bannedDto = _mapper.Map<BannedDTO>(bannedDtoC);
                bannedDto.UserIdModerator = userId;

                var created = await _bannedService.CreateAsync(bannedDto);
                return created != null
                    ? CreatedAtAction(nameof(GetBannedById), new { id = created.Id }, created)
                    : StatusCode(500, "Ошибка при создании бана");
            }

            return BadRequest("у вас недостаточно прав для выполения данного действия");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanned(Guid id, [FromBody] BannedDTO bannedDto)
        {
            if (id != bannedDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных");

            var updated = await _bannedService.UpdateAsync(id, bannedDto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanned(Guid id)
        {
            var deleted = await _bannedService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}