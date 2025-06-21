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
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;

namespace OrganizerEventService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService service;

        public OrganizerController(IOrganizerService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Получение всех организаторов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizerDTO>>> GetAll()
        {
            return Ok(await service.GetAllAsync());
        }

        /// <summary>
        /// Создание нового организатора
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<OrganizerDTO>> Create(OrganizerDTO dto)
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

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizerDTO>> GetById(Guid id)
        {
            var model = await service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        /// <summary>
        /// Получение студий по ID собственника
        /// </summary>
        /// <param name="id">Идентификатор собственника (OwnerId)</param>
        /// <returns>Организатор</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrganizerDTO>>> GetByUserId(Guid id)
        {
            try
            {
                var result = await service.GetByUserId(id);
                if (result == null || !result.Any())
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Обновление организатора
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, OrganizerDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var existingOrganizerDTO = await service.GetByIdAsync(id);
            if (existingOrganizerDTO == null)
            {
                return NotFound();
            }
            await service.UpdateAsync(dto);

            return Ok(dto);
        }

        /// <summary>
        /// Удаление организатора
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await service.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Фильтрация организаторов
        /// </summary>
        /// <returns>Список событий</returns>
        [HttpPost]
        public async Task<ActionResult> Filter([FromBody] ParamQueryOrganizer param)
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
