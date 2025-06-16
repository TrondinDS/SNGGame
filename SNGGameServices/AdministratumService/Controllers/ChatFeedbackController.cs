using AdministratumService.DB.DTO.ChatFeedback;
using AdministratumService.DB.Models;
using AdministratumService.Repository;
using AdministratumService.Services.Interfaces;
using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.GenericService;
using Microsoft.AspNetCore.Mvc;

namespace AdministratumService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatFeedbackController : Controller
    {
        private readonly IChatFeedbackService service;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public ChatFeedbackController(IChatFeedbackService service, IMapper mapper, ILogger<ChatFeedbackController> logger)
        {
            this.service = service;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Получение всех тикетов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComplainTicketDTO>>> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<ChatFeedbackDTO>> Create(ChatFeedbackDTO dto)
        {
            try
            {
                await service.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при создании чата");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChatFeedbackDTO>> GetById(Guid id)
        {
            var model = await service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            var res = mapper.Map<ChatFeedbackDTO>(model);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ChatFeedbackDTO dto)
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
                    //return NotFound();
                    return Ok();
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var model = await service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            await service.DeleteAsync(id);
            return Ok();
        }
    }
}