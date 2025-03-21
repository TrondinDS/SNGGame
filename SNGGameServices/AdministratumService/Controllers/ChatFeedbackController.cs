using AdministratumService.DB.DTO.ChatFeedback;
using AdministratumService.DB.Models;
using AdministratumService.Repository;
using AutoMapper;
using Library.GenericService;
using Microsoft.AspNetCore.Mvc;

namespace AdministratumService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatFeedbackController : Controller
    {
        private readonly CrudGenericService<ChatFeedback, int, ChatFeedbackRepository> service;
        private readonly IMapper mapper;

        public ChatFeedbackController(CrudGenericService<ChatFeedback, int, ChatFeedbackRepository> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ChatFeedbackIdDTO>> Create(CreateChatFeedbackDTO DTO)
        {
            var model = mapper.Map<ChatFeedback>(DTO);
            await service.AddAsync(model);
            var res = mapper.Map<ChatFeedbackIdDTO>(model);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdChatFeedbackDTO>> GetById(int id)
        {
            var model = await service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var res = mapper.Map<GetByIdChatFeedbackDTO>(model);
            return Ok(res);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateChatFeedbackDTO DTO)
        {
            var model = await service.GetByIdAsync(DTO.Id);
            if (model == null)
            {
                return NotFound();
            }
            await service.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteChatFeedbackDTO DTO)
        {
            return BadRequest("операции удаления для чата недоступна");
        }
    }
}