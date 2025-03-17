using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UserActivityService.DB.Models;
using UserActivityService.Services.Interfaces;
using UserActivityService.DB.DTO.UserReaction;

namespace UserActivityService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserReactionController : ControllerBase
    {
        private readonly IUserReactionService userReactionService;
        private readonly IMapper mapper;

        public UserReactionController(IUserReactionService userReactionService, IMapper mapper)
        {
            this.userReactionService = userReactionService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех реакций пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReactionDTO>>> GetAllReactions()
        {
            var reactions = await userReactionService.GetAllAsync();
            var reactionsDTO = mapper.Map<IEnumerable<UserReactionDTO>>(reactions);
            return Ok(reactionsDTO);
        }

        /// <summary>
        /// Получение реакции по ID
        /// </summary>
        /// <param name="id">Идентификатор реакции</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReactionDTO>> GetReactionById(int id)
        {
            var reaction = await userReactionService.GetByIdAsync(id);
            if (reaction == null)
            {
                return NotFound();
            }
            var reactionDTO = mapper.Map<UserReactionDTO>(reaction);
            return Ok(reactionDTO);
        }

        /// <summary>
        /// Создание новой реакции
        /// </summary>
        /// <param name="reactionDTO">Данные для создания новой реакции</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateReaction(UserReactionDTO reactionDTO)
        {
            var reaction = mapper.Map<UserReaction>(reactionDTO);
            await userReactionService.AddAsync(reaction);
            var reactionResultDTO = mapper.Map<UserReactionDTO>(reaction);
            return CreatedAtAction(nameof(GetReactionById), new { id = reaction.Id }, reactionResultDTO);
        }

        /// <summary>
        /// Обновление реакции
        /// </summary>
        /// <param name="id">Идентификатор реакции</param>
        /// <param name="reactionDTO">Обновленные данные реакции</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReaction(int id, UserReactionDTO reactionDTO)
        {
            if (id != reactionDTO.Id)
            {
                return BadRequest();
            }

            var existingReaction = await userReactionService.GetByIdAsync(id);
            if (existingReaction == null)
            {
                return NotFound();
            }

            var reaction = mapper.Map<UserReaction>(reactionDTO);
            await userReactionService.UpdateAsync(reaction);
            return Ok(reaction);
        }

        /// <summary>
        /// Удаление реакции
        /// </summary>
        /// <param name="id">Идентификатор реакции</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReaction(int id)
        {
            await userReactionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
