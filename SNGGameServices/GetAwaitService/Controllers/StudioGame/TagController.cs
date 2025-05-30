using GetAwaitService.Services.StudioGameService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _service.GetAllAsync();
            return tags != null ? Ok(tags) : StatusCode(500, "Ошибка при получении списка тегов.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(Guid id)
        {
            var tag = await _service.GetByIdAsync(id);
            return tag != null ? Ok(tag) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] TagDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return created != null
                ? CreatedAtAction(nameof(GetTagById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании тега.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(Guid id, [FromBody] TagDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID в запросе не совпадает с ID в теле запроса.");

            var updated = await _service.UpdateAsync(dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}