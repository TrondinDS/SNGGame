using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrganizerEventService.DB.DTO.Event;
using OrganizerEventService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using OrganizerEventService.DB.DTO.Organizer;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;

namespace OrganizerEventService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService service;

        public EventController(IEventService service)
        {
            this.service = service;
        }


        /// <summary>
        /// Получение всех событий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }

        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EventDTO>> Create(EventDTO dto)
        {
            try
            {
                await service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Получение события по идентификатору
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetById(Guid id)
        {
            var model = await service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        /// <summary>
        /// Обновление информации о событиии
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, EventDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var existingEventDTO = await service.GetByIdAsync(id);
            if (existingEventDTO == null)
            {
                return NotFound();
            }
            await service.UpdateAsync(dto);

            return Ok(dto);
        }

        /// <summary>
        /// Удаление события
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await service.DeleteAsync(id);
            return Ok();
        }
    }
}