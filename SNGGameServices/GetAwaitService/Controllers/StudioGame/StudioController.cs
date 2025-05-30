using GetAwaitService.Services.StudioGameService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.StudioGame
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudioController : ControllerBase
    {
        private readonly IStudioService _service;

        public StudioController(IStudioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudios()
        {
            var studios = await _service.GetAllAsync();
            return studios != null ? Ok(studios) : StatusCode(500, "Ошибка при получении списка студий.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudioById(Guid id)
        {
            var studio = await _service.GetByIdAsync(id);
            return studio != null ? Ok(studio) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudioByUserId(Guid id)
        {
            var studio = await _service.GetStudioByUserIdAsync(id);
            return studio != null ? Ok(studio) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudio([FromBody] StudioDTO dto)
        {
            var created = await _service.CreateAsync(dto);
            return created != null
                ? CreatedAtAction(nameof(GetStudioById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании студии.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudio(Guid id, [FromBody] StudioDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID в запросе не совпадает с ID в теле запроса.");

            var updated = await _service.UpdateAsync(dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudio(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}