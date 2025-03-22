using AutoMapper;
using Library.Generics.GenericService;
using Microsoft.AspNetCore.Mvc;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Repository;
using OrganizerEventService.DB.DTO.Event;
using OrganizerEventService.Enums;

namespace OrganizerEventService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly CrudGenericService<Event, int, EventRepository> service;
        private readonly IMapper mapper;

        public EventController(CrudGenericService<Event, int, EventRepository> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<EventIdDTO>> Create(CreateEventDTO dto)
        {
            var model = mapper.Map<Event>(dto);
            await service.AddAsync(model);
            var res = mapper.Map<EventIdDTO>(model);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdEventDTO>> GetById(int id)
        {
            var model = await service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            if (model.IsDeleted)
            {
                return Problem("event was deleted");
            }
            var res = mapper.Map<GetByIdEventDTO>(model);
            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateEventDTO dto)
        {
            var model = await service.GetByIdAsync(dto.Id);
            if (model == null)
            {
                return NotFound();
            }
            if (!Library.Utils.Enums.HasField<EventStatus>(dto.Status))
            {
                return ValidationProblem($"Does not have {dto.Status} status, only have: {Library.Utils.Enums.GetFieldsAsString<EventStatus>()}");
            }
            await service.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteEventDTO dto)
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
