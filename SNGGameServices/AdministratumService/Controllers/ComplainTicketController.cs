using AdministratumService.DB.DTO.ComplainTicket;
using AdministratumService.DB.Models;
using AdministratumService.Enums;
using AdministratumService.Repository;
using AdministratumService.Services.Interfaces;
using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Library.Generics.GenericService;
using Library.Generics.Query.QueryModels.Administratum;
using Microsoft.AspNetCore.Mvc;

namespace AdministratumService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComplainTicketController : Controller
    {
        private readonly IComplainTicketService service;
        private readonly IMapper mapper;

        public ComplainTicketController(IComplainTicketService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
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

        /// <summary>
        /// Получение тикета по ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplainTicketDTO>> GetById(Guid id)
        {
            var ticket = await service.GetByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        /// <summary>
        /// Создание нового тикета
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(ComplainTicketDTO dto)
        {
            await service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Обновление тикета
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, ComplainTicketDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var existingUser = await service.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            await service.UpdateAsync(dto);

            return Ok(dto);
        }

        /// <summary>
        /// Удаление тикета
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await service.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Фильтрация жалоб
        /// </summary>
        /// <returns>Список событий</returns>
        [HttpPost]
        public async Task<ActionResult> Filter([FromBody] ParamQueryComplainTicket param)
        {
            try
            {
                var elems = await service.Filter(param);
                return Ok(elems);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }
    }
}
