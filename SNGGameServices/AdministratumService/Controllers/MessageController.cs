using AdministratumService.DB.DTO.ChatFeedback;
using AdministratumService.DB.DTO.Message;
using AdministratumService.DB.Models;
using AdministratumService.Repository;
using AutoMapper;
using Library.Generics.GenericService;
using Microsoft.AspNetCore.Mvc;

namespace AdministratumService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly CrudGenericService<Message, int, MessageRepository> service;
        private readonly IMapper mapper;

        public MessageController(CrudGenericService<Message, int, MessageRepository> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageIdDTO>> Create(CreateMessageDTO dto)
        {
            var model = mapper.Map<Message>(dto);
            await service.AddAsync(model);
            var res = mapper.Map<MessageIdDTO>(model);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdMessageDTO>> GetById(int id)
        {
            var model = await service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var res = mapper.Map<GetByIdMessageDTO>(model);
            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateMessageDTO dto)
        {
            return BadRequest("операция обновления для сообщений чата недоступна");
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteMessageDTO dto)
        {
            var model = await service.GetByIdAsync(dto.Id);
            if (model == null)
            {
                return NotFound();
            }
            if (model.IsDeleted)
            {
                return Problem("сообщение уже удалено");
            }
            model.DateDeleted = DateTime.UtcNow;
            return Ok();
        }
    }
}
