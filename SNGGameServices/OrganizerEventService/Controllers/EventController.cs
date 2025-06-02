using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizerEventService.DB.DTO.Event;
using OrganizerEventService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using OrganizerEventService.DB.DTO.Organizer;

namespace OrganizerEventService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService service;
        private readonly IMapper mapper;
        private readonly ILogger<EventController> logger;

        public EventController(IEventService service, IMapper mapper, ILogger<EventController> logger)
        {
            this.service = service;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<EventIdDTO>> Create(EventDTO dto)
        {
            try
            {
                await service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при создании игры");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdEventDTO>> GetById(Guid id)
        {
            var model = await service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            if (model.IsDeleted)
            {
                return Problem("organizer was deleted");
            }
            var res = mapper.Map<GetByIdOrganizerDTO>(model);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, EventDTO dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    return BadRequest();
                }

                var existed = await service.GetByIdAsync(id);
                if (existed == null)
                {
                    return NotFound();
                }

                await service.UpdateAsync(dto);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при обновлении игры с ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(EventDTO dto)
        {
            var model = await service.GetByIdAsync(dto.Id);
            if (model == null)
            {
                return NotFound();
            }
            if (model.IsDeleted)
            {
                return Problem("event already was deleted");
            }
            model.DateDeleted = DateTime.UtcNow;
            return Ok();
        }
    }
}