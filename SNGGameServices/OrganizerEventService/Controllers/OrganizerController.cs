using AutoMapper;
using Library.Generics.GenericService;
using Microsoft.AspNetCore.Mvc;
using OrganizerEventService.DB.DTO.Event;
using OrganizerEventService.DB.DTO.Organizer;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Enums;
using OrganizerEventService.Repository;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using OrganizerEventService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;

namespace OrganizerEventService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService service;
        //private readonly CrudGenericService<Organizer, Guid, OrganizerRepository> service;
        private readonly IMapper mapper;
        private readonly ILogger<OrganizerController> logger;

        public OrganizerController(IOrganizerService service, IMapper mapper, ILogger<OrganizerController> logger)
        {
            this.service = service;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<OrganizerIdDTO>> Create(OrganizerDTO dto)
        {
            try
            {
                await service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при создании организатора");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdOrganizerDTO>> GetById(Guid id)
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
        public async Task<ActionResult> Update(Guid id, OrganizerDTO dto)
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
        public async Task<ActionResult> Delete(OrganizerDTO dto)
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
