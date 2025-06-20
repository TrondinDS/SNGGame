using GetAwaitService.Services.OrganizerEventService.Interfaces;

namespace GetAwaitService.Controllers.OrganizerEvent;

using AutoMapper;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventService _service;
    private readonly IUserAccessRightsService _userAccessRightsService;
    private readonly IMapper _mapper;

    public EventController(
        IEventService service,
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
        return elems != null ? Ok(elems) : StatusCode(500, "Ошибка при получении списка событий.");
    }

    [HttpPost]
    public async Task<IActionResult> Filter([FromBody] ParamQueryEvent param)
    {
        var elems = await _service.Filter(param);
        return elems != null ? Ok(elems) : StatusCode(500, "Ошибка при фильтрации событий.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var organizer = await _service.GetById(id);
        return organizer != null ? Ok(organizer) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EventDTO dto)
    {
        var userIdClaim = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return BadRequest("User ID не найден в claims.");

        var hasRights = await _userAccessRightsService
            .CheckUserRightsModerAndAdminOrganizerAsync(userId, dto.OrganizerEventId);
        if (!hasRights)
            return BadRequest("у вас недостаточно прав для выполения данного действия");

        var created = await _service.Create(dto);
        return created != null
            ? CreatedAtAction(nameof(GetById), new { id = created.Id }, created)
            : StatusCode(500, "Ошибка при создании события.");
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] EventDTO dto)
    {
        var userIdClaim = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return BadRequest("User ID не найден в claims.");

        var hasRights = await _userAccessRightsService
            .CheckUserRightsModerAndAdminOrganizerAsync(userId, dto.OrganizerEventId);
        if (!hasRights)
            return BadRequest("у вас недостаточно прав для выполения данного действия");

        var updated = await _service.Update(dto);
        return updated ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userIdClaim = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return BadRequest("User ID не найден в claims.");

        var hasRights = await _userAccessRightsService
            .CheckUserRightsModerAndAdminEventAsync(userId, id);
        if (!hasRights)
            return BadRequest("у вас недостаточно прав для выполения данного действия");

        var deleted = await _service.Delete(id);
        return deleted ? NoContent() : NotFound();
    }
}
