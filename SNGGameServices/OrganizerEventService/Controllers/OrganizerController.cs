using AutoMapper;
using Library.Generics.GenericService;
using Microsoft.AspNetCore.Mvc;
using OrganizerEventService.DB.DTO.Event;
using OrganizerEventService.DB.DTO.Organizer;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Enums;
using OrganizerEventService.Repository;

namespace OrganizerEventService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizerController : Controller
    {
        private readonly CrudGenericService<Organizer, Guid, OrganizerRepository> service;
        private readonly IMapper mapper;

        public OrganizerController(CrudGenericService<Organizer, Guid, OrganizerRepository> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrganizerIdDTO>> Create(CreateOrganizerDTO dto)
        {
            var model = mapper.Map<Organizer>(dto);
            model.IsPublicationAllowed = true;
            await service.AddAsync(model);
            var res = mapper.Map<OrganizerIdDTO>(model);
            return Ok(res);
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

        [HttpPut]
        public async Task<ActionResult> Update(UpdateOrganizerDTO dto)
        {
            var model = await service.GetByIdAsync(dto.Id);
            if (model == null)
            {
                return NotFound();
            }
            await service.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteOrganizerDTO dto)
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
