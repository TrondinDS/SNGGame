using AutoMapper;
using GetAwaitService.Services.ChatFeedbackService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.OrganizerEvent
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly IOrganizerService _service;
        private readonly IUserAccessRightsService _userAccessRightsService;
        private readonly IMapper _mapper;

        public OrganizerController(
            IOrganizerService service,
            IUserAccessRightsService userAccessRightsService,
            IMapper mapper)
        {
            _service = service;
            _userAccessRightsService = userAccessRightsService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizers = await _service.GetAll();
            return organizers != null ? Ok(organizers) : StatusCode(500, "Ошибка при получении списка организаторов.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var organizer = await _service.GetById(id);
            return organizer != null ? Ok(organizer) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrganizerDTO dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID не найден в claims.");

            // Пример установки владельца или создателя
            var organizerDto = _mapper.Map<OrganizerDTO>(dto);
            organizerDto.CreatorId = userId;
            organizerDto.OwnerId = userId;

            var created = await _service.Create(organizerDto);
            return created != null
                ? CreatedAtAction(nameof(GetById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании организатора.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrganizerDTO dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID не найден в claims.");

            var updated = await _service.Update(dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID не найден в claims.");

            var deleted = await _service.Delete(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
