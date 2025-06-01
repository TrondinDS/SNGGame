using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.DB.Model;
using StudioGameService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudioGameService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly IStudioService studioService;
        private readonly IMapper mapper;
        private readonly ILogger<StudioController> logger;

        public StudioController(IStudioService studioService, IMapper mapper, ILogger<StudioController> logger)
        {
            this.studioService = studioService;
            this.mapper = mapper;
            this.logger = logger;
        }

        /// <summary>
        /// Получение всех студий
        /// </summary>
        /// <returns>Список всех студий</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudioDTO>>> GetAllStudios()
        {
            try
            {
                var result = await studioService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при получении всех студий");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Получение студии по ID
        /// </summary>
        /// <param name="id">Идентификатор студии</param>
        /// <returns>Студия с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudioDTO>> GetStudioById(Guid id)
        {
            try
            {
                var result = await studioService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при получении студии по ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        } 
        
        /// <summary>
        /// Получение студий по ID администратора
        /// </summary>
        /// <param name="id">Идентификатор администратора (OwnerId)</param>
        /// <returns>Студии</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<StudioDTO>>> GetStudioByUserId(Guid id)
        {
            try
            {
                var result = await studioService.GetStudioByUserIdAsync(id);
                if (result == null || !result.Any())
                {
                    return NotFound();
                }

                var mapResult = mapper.Map<IEnumerable<StudioDTO>>(result);
                return Ok(mapResult);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при получении студии по ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Создание новой студии
        /// </summary>
        /// <param name="studioCreateDTO">Данные для создания студии</param>
        /// <returns>Созданная студия</returns>
        [HttpPost]
        public async Task<ActionResult> CreateStudio(StudioDTO studioDTO)
        {
            try
            {
                await studioService.AddAsync(studioDTO);
                return CreatedAtAction(nameof(GetStudioById), new { id = studioDTO.Id }, studioDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при создании студии");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Обновление студии
        /// </summary>
        /// <param name="id">Идентификатор студии</param>
        /// <param name="studioDTO">Обновленные данные студии</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudio(Guid id, StudioDTO studioDTO)
        {
            try
            {
                if (id != studioDTO.Id)
                {
                    return BadRequest();
                }

                var existingStudio = await studioService.GetByIdAsync(id);
                if (existingStudio == null)
                {
                    return NotFound();
                }

                await studioService.UpdateAsync(studioDTO);
                return Ok(studioDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при обновлении студии с ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }

        /// <summary>
        /// Удаление студии
        /// </summary>
        /// <param name="id">Идентификатор студии</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudio(Guid id)
        {
            try
            {
                await studioService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ошибка при удалении студии с ID: {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Произошла внутренняя ошибка сервера", details = ex.Message });
            }
        }
    }
}