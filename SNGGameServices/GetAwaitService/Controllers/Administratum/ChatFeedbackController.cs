using AutoMapper;
using GetAwaitService.Services.ChatFeedbackService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.Administratum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatFeedbackController : ControllerBase
    {
        private readonly IChatFeedbackService _service;
        private readonly IUserAccessRightsService _userAccessRightsService;
        private readonly IMapper _mapper;

        public ChatFeedbackController(
            IChatFeedbackService service,
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
            var elems = await _service.GetAll();
            return elems != null ? Ok(elems) : StatusCode(500, "Ошибка при получении списка чатов.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChatFeedbackDTO dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            //var studioDto = _mapper.Map<ChatFeedbackDTO>(dto);
            //studioDto.CreatorId = userId;
            //studioDto.OwnerId = userId;

            var created = await _service.Create(dto);
            return created != null
                ? CreatedAtAction(nameof(GetById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании чата.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ChatFeedbackDTO dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("User ID not found in claims.");
            }
            var updated = await _service.Update(dto);
            return Ok();

            //var check = await _userAccessRightsService.ChekUserRightsModerAndAdminStudioAsync(userId, dto.Id);
            //if (check == true)
            //{
            //    var updated = await _service.Update(id, dto);
            //    return updated ? Ok() : NotFound();
            //}

            //return BadRequest("Отказ доступа");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("User ID not found in claims.");
            }
            var deleted = await _service.Delete(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
