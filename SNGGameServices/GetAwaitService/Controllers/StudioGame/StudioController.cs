using AutoMapper;
using GetAwaitService.Services.StudioGameService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly IStudioService _service;
        private readonly IUserAccessRightsService _userAccessRightsService;
        private readonly IMapper _mapper;

        public StudioController(
            IStudioService service,
            IUserAccessRightsService userAccessRightsService,
            IMapper mapper)
        {
            _service = service;
            _userAccessRightsService = userAccessRightsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudios()
        {
            var studios = await _service.GetAllAsync();
            return studios != null ? Ok(studios) : StatusCode(500, "Ошибка при получении списка студий.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudioById(Guid id)
        {
            var studio = await _service.GetByIdAsync(id);
            return studio != null ? Ok(studio) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudioByUserId(Guid id)
        {
            var studio = await _service.GetStudioByUserIdAsync(id);
            return studio != null ? Ok(studio) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudio([FromBody] StudioCreateDTO dtoC)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

                var studioDto = _mapper.Map<StudioDTO>(dtoC);
                studioDto.CreatorId = userId;
                studioDto.OwnerId = userId;
                
                var created = await _service.CreateAsync(studioDto);
                return created != null
                    ? CreatedAtAction(nameof(GetStudioById), new { id = created.Id }, created)
                    : StatusCode(500, "Ошибка при создании студии.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudio([FromBody] StudioDTO dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("User ID not found in claims.");
            }

            var check = await _userAccessRightsService.ChekUserRightsModerAndAdminStudioAsync(userId, dto.Id);
            if (check == true)
            {
                var updated = await _service.UpdateAsync(dto);
                return updated ? Ok() : NotFound();
            }

            return BadRequest("Отказ доступа");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudio(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}