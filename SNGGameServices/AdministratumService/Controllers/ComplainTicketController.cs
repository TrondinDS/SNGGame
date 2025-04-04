using AdministratumService.DB.DTO.ComplainTicket;
using AdministratumService.DB.Models;
using AdministratumService.Enums;
using AdministratumService.Repository;
using AutoMapper;
using Library.Generics.GenericService;
using Microsoft.AspNetCore.Mvc;

namespace AdministratumService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComplainTicketController : Controller
    {
        private readonly CrudGenericService<ComplainTicket, Guid, ComplainTicketRepository> service;
        private readonly IMapper mapper;

        public ComplainTicketController(CrudGenericService<ComplainTicket, Guid, ComplainTicketRepository> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ComplainTicketIdDTO>> Create(CreateComplainTicketDTO dto)
        {
            var model = mapper.Map<ComplainTicket>(dto);
            model.Status = nameof(ComplainType.Created);
            await service.AddAsync(model);
            var res = mapper.Map<ComplainTicketIdDTO>(model);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdComplainTicketDTO>> GetById(Guid id)
        {
            var model = await service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var res = mapper.Map<GetByIdComplainTicketDTO>(model);
            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateComplainTicketDTO dto)
        {
            var model = await service.GetByIdAsync(dto.Id);
            if (model == null)
            {
                return NotFound();
            }
            if (!Library.Utils.Enums.HasField<ComplainType>(dto.Status))
            {
                return ValidationProblem($"Нет статуса {dto.Status}, есть только: {Library.Utils.Enums.GetFieldsAsString<ComplainType>()}");
            }
            if (dto.Status != model.Status)
            {
                model.StatusUpdateDatetime = DateTime.UtcNow;
            }
            await service.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteComplainTicketDTO dto)
        {
            var model = await service.GetByIdAsync(dto.Id);
            if (model == null)
            {
                return NotFound();
            }
            if (model.IsDeleted)
            {
                return Problem("жалоба уже удалена");
            }
            model.DateDeleted = DateTime.UtcNow;
            return Ok();
        }
    }
}
