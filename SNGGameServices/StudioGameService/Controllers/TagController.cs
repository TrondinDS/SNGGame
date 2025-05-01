using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.DB.Model;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;
        private readonly IMapper mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            this.tagService = tagService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех тегов
        /// </summary>
        /// <returns>Список всех тегов</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAllTags()
        {
            var tags = await tagService.GetAllAsync();
            var tagDTOs = mapper.Map<IEnumerable<TagDTO>>(tags);
            return Ok(tagDTOs);
        }

        /// <summary>
        /// Получение тега по ID
        /// </summary>
        /// <param name="id">Идентификатор тега</param>
        /// <returns>Тег с указанным ID</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TagDTO>> GetTagById(Guid id)
        {
            var tag = await tagService.GetByIdAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            var tagDTO = mapper.Map<TagDTO>(tag);
            return Ok(tagDTO);
        }

        /// <summary>
        /// Создание нового тега
        /// </summary>
        /// <param name="tagCreateDTO">Данные для создания тега</param>
        /// <returns>Созданный тег</returns>
        [HttpPost]
        public async Task<ActionResult> CreateTag(TagDTO tagCreateDTO)
        {
            var tag = mapper.Map<Tag>(tagCreateDTO);
            await tagService.AddAsync(tag);
            var tagResultDTO = mapper.Map<TagDTO>(tag);
            return CreatedAtAction(nameof(GetTagById), new { id = tag.Id }, tagResultDTO);
        }

        /// <summary>
        /// Обновление тега
        /// </summary>
        /// <param name="id">Идентификатор тега</param>
        /// <param name="tagDTO">Обновленные данные тега</param>
        /// <returns>Результат обновления</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTag(Guid id, TagDTO tagDTO)
        {
            if (id != tagDTO.Id)
            {
                return BadRequest();
            }

            var existingTag = await tagService.GetByIdAsync(id);
            if (existingTag == null)
            {
                return NotFound();
            }

            var tag = mapper.Map<Tag>(tagDTO);
            await tagService.UpdateAsync(tag);
            return Ok(tag);
        }

        /// <summary>
        /// Удаление тега
        /// </summary>
        /// <param name="id">Идентификатор тега</param>
        /// <returns>Результат удаления</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTag(Guid id)
        {
            await tagService.DeleteAsync(id);
            return NoContent();
        }
    }
}
