using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.DB.DTO.UserSubscription;
using UserService.DB.Models;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly IUserSubscriptionService userSubscriptionService;
        private readonly IMapper mapper;

        public UserSubscriptionController(IUserSubscriptionService userSubscriptionService, IMapper mapper)
        {
            this.userSubscriptionService = userSubscriptionService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение все подписки пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSubscriptionDTO>>> GetAllUserSubscription()
        {
            var usersSubscriptions = await userSubscriptionService.GetAllAsync();
            var usersSubscriptionsDTO = mapper.Map<IEnumerable<UserSubscriptionDTO>>(usersSubscriptions);
            return Ok(usersSubscriptionsDTO);
        }

        /// <summary>
        /// Получение подписки пользователя по ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSubscriptionDTO>> GetUserSubscriptionById(int id)
        {
            var userSub = await userSubscriptionService.GetByIdAsync(id);
            if (userSub == null)
            {
                return NotFound();
            }
            var userSubDTO = mapper.Map<UserSubscriptionDTO>(userSub);
            return Ok(userSubDTO);
        }

        /// <summary>
        /// Создание новой подписки пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateUserSubscription(UserSubscriptionDTO userSubscriptionDTO)
        {
            var userSub = mapper.Map<UserSubscription>(userSubscriptionDTO);
            await userSubscriptionService.AddAsync(userSub);
            var userResultDTO = mapper.Map<UserSubscriptionDTO>(userSubscriptionDTO);
            return CreatedAtAction(nameof(GetUserSubscriptionById), new { id = userSub.Id }, userResultDTO);
        }

        /// <summary>
        /// Обновление подписки пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserSubscription(int id, UserSubscriptionDTO userSubscriptionDTO)
        {
            if (id != userSubscriptionDTO.Id)
            {
                return BadRequest();
            }

            var existingUserSubscriptio = await userSubscriptionService.GetByIdAsync(id);
            if (existingUserSubscriptio == null)
            {
                return NotFound();
            }

            var userSub = mapper.Map<UserSubscription>(userSubscriptionDTO);
            await userSubscriptionService.UpdateAsync(userSub);
            return Ok(userSub);
        }

        /// <summary>
        /// Удаление подписки пользователя
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserSubscription(int id)
        {
            await userSubscriptionService.DeleteAsync(id);
            return NoContent();
        }
    }
}
