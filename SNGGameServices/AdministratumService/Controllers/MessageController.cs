﻿using AdministratumService.DB.DTO.ChatFeedback;
using AdministratumService.DB.DTO.Message;
using AdministratumService.DB.Models;
using AdministratumService.Repository;
using AdministratumService.Services.Interfaces;
using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;
using Library.Generics.GenericService;
using Library.Generics.Query.QueryModels.Administratum;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.AspNetCore.Mvc;

namespace AdministratumService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly IMessageService service;
        private readonly IMapper mapper;

        public MessageController(IMessageService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех тикетов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }

        /// <summary>
        /// Получение тикета по ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageDTO>> GetById(Guid id)
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
        public async Task<ActionResult> Create(MessageDTO dto)
        {
            await service.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Обновление тикета
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, MessageDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var existingMessageDTO = await service.GetByIdAsync(id);
            if (existingMessageDTO == null)
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
            return NoContent();
        }

        /// <summary>
        /// Фильтрация сообщений
        /// </summary>
        /// <returns>Список событий</returns>
        [HttpPost]
        public async Task<ActionResult> Filter([FromBody] ParamQueryMessage param)
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
