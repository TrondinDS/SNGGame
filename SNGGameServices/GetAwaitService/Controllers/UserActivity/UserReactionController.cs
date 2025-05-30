using GetAwaitService.Services.UserActivityService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.UserReaction;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.UserActivity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserReactionController : ControllerBase
    {
        private readonly IUserReactionApiService _reactionService;

        public UserReactionController(IUserReactionApiService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReactions()
        {
            var reactions = await _reactionService.GetAllAsync();
            return reactions != null ? Ok(reactions) : StatusCode(500, "Ошибка при получении списка реакций.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReactionById(Guid id)
        {
            var reaction = await _reactionService.GetByIdAsync(id);
            return reaction != null ? Ok(reaction) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReaction([FromBody] UserReactionDTO reactionDto)
        {
            var created = await _reactionService.CreateAsync(reactionDto);
            return created != null
                ? CreatedAtAction(nameof(GetReactionById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании реакции.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReaction(Guid id, [FromBody] UserReactionDTO reactionDto)
        {
            if (id != reactionDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var updated = await _reactionService.UpdateAsync(id, reactionDto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReaction(Guid id)
        {
            var deleted = await _reactionService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}