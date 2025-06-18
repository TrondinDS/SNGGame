using AutoMapper;
using GetAwaitService.Services.ChatFeedbackService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;
using Library.Generics.Query.QueryModels.Administratum;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.Administratum
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComplainTicketController : ControllerBase
    {
        private readonly IComplainTicketService _service;
        private readonly IUserAccessRightsService _userAccessRightsService;
        private readonly IMapper _mapper;

        public ComplainTicketController(
            IComplainTicketService service,
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
            var tickets = await _service.GetAll();
            return tickets != null ? Ok(tickets) : StatusCode(500, "Ошибка при получении списка жалоб.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticket = await _service.GetById(id);
            return ticket != null ? Ok(ticket) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComplainTicketDTO dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            // Если требуется установить автора или владельца
            // dto.CreatorId = userId;
            // dto.OwnerId = userId;

            var created = await _service.Create(dto);
            return created != null
                ? CreatedAtAction(nameof(GetById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании тикета жалобы.");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ComplainTicketDTO dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("User ID not found in claims.");
            }

            // Пример проверки прав (если нужно ограничивать доступ к обновлению)
            // var check = await _userAccessRightsService.CheckUserRightsModerAndAdminStudioAsync(userId, dto.StudioId);
            // if (!check)
            //     return BadRequest("У вас недостаточно прав для выполнения данного действия.");

            var updated = await _service.Update(dto);
            return Ok(updated);

            // Ниже пример с проверкой по ID (если нужен более строгий контроль)
            // var updated = await _service.Update(dto.Id, dto);
            // return updated ? Ok() : NotFound();
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

        [HttpPost]
        public async Task<IActionResult> Filter([FromBody] ParamQueryComplainTicket param)
        {
            var elems = await _service.Filter(param);
            return elems != null ? Ok(elems) : StatusCode(500, "Ошибка при фильтрации жалоб.");
        }
    }
}
