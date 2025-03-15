using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.DB.DTO.Studio;
using StudioGameService.DB.Model;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly IStudioService studioService;
        private readonly IMapper mapper;

        public StudioController(IStudioService studioService, IMapper mapper)
        {
            this.studioService = studioService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех студий
        /// </summary>
        /// <returns>Список всех студий</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudioDTO>>> GetAllStudios()
        {
            var studios = await studioService.GetAllAsync();
            var studioDTOs = mapper.Map<IEnumerable<StudioDTO>>(studios);
            return Ok(studioDTOs);
        }

        /// <summary>
        /// Получение студии по ID
        /// </summary>
        /// <param name="id">Идентификатор студии</param>
        /// <returns>Студия с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudioDTO>> GetStudioById(int id)
        {
            var studio = await studioService.GetByIdAsync(id);
            if (studio == null)
            {
                return NotFound();
            }
            var studioDTO = mapper.Map<StudioDTO>(studio);
            return Ok(studioDTO);
        }

        /// <summary>
        /// Создание новой студии
        /// </summary>
        /// <param name="studioCreateDTO">Данные для создания студии</param>
        /// <returns>Созданная студия</returns>
        [HttpPost]
        public async Task<ActionResult> CreateStudio(StudioDTO studioCreateDTO)
        {
            var studio = mapper.Map<Studio>(studioCreateDTO);
            await studioService.AddAsync(studio);
            var studioResultDTO = mapper.Map<StudioDTO>(studio);
            return CreatedAtAction(nameof(GetStudioById), new { id = studio.Id }, studioResultDTO);
        }

        /// <summary>
        /// Обновление студии
        /// </summary>
        /// <param name="id">Идентификатор студии</param>
        /// <param name="studioDTO">Обновленные данные студии</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudio(int id, StudioDTO studioDTO)
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

            var studio = mapper.Map<Studio>(studioDTO);
            await studioService.UpdateAsync(studio);
            return Ok(studio);
        }

        /// <summary>
        /// Удаление студии
        /// </summary>
        /// <param name="id">Идентификатор студии</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudio(int id)
        {
            await studioService.DeleteAsync(id);
            return NoContent();
        }
    }
}