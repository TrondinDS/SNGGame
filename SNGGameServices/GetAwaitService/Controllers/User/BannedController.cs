using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BannedController : ControllerBase
    {
        private readonly IBannedApiService _bannedService;

        public BannedController(IBannedApiService bannedService)
        {
            _bannedService = bannedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBanned()
        {
            var result = await _bannedService.GetAllAsync();
            return result != null ? Ok(result) : StatusCode(500, "Ошибка при получении списка банов");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBannedById(Guid id)
        {
            var result = await _bannedService.GetByIdAsync(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanned([FromBody] BannedDTO bannedDto)
        {
            var created = await _bannedService.CreateAsync(bannedDto);
            return created != null
                ? CreatedAtAction(nameof(GetBannedById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании бана");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanned(Guid id, [FromBody] BannedDTO bannedDto)
        {
            if (id != bannedDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных");

            var updated = await _bannedService.UpdateAsync(id, bannedDto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanned(Guid id)
        {
            var deleted = await _bannedService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}