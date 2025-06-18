using AutoMapper;
using GetAwaitService.Services.ChatFeedbackService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;
using Library.Generics.Query.QueryModels.Administratum;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.Administratum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;
        private readonly IUserAccessRightsService _userAccessRightsService;
        private readonly IMapper _mapper;

        public MessageController(
            IMessageService service,
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
            var messages = await _service.GetAll();
            return messages != null ? Ok(messages) : StatusCode(500, "Ошибка при получении списка сообщений.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var message = await _service.GetById(id);
            return message != null ? Ok(message) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MessageDTO dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID не найден в claims.");

            var created = await _service.Create(dto);
            return created != null
                ? CreatedAtAction(nameof(GetById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании сообщения.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] MessageDTO dto)
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

        [HttpPost]
        public async Task<IActionResult> Filter([FromBody] ParamQueryMessage param)
        {
            var elems = await _service.Filter(param);
            return elems != null ? Ok(elems) : StatusCode(500, "Ошибка при фильтрации событий.");
        }
    }
}
